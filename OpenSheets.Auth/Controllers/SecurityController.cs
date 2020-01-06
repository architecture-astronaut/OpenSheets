using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Web.Configuration;
using System.Web.Http;
using OpenSheets.Auth.Requests;
using OpenSheets.Auth.Responses;
using OpenSheets.Common;
using OpenSheets.Contracts.Commands;
using OpenSheets.Contracts.Events;
using OpenSheets.Contracts.Events.Principal;
using OpenSheets.Contracts.Requests;
using OpenSheets.Contracts.Responses;
using OpenSheets.Core;
using OpenSheets.Core.Hexagon;
using OpenSheets.Storage;
using OpenSheets.Web;

namespace OpenSheets.Auth.Controllers
{
    public class SecurityController : ContextedController
    {
        private readonly IServiceRouter _router;

        public SecurityController(IWebContext context, IServiceRouter router) : base(context)
        {
            _router = router;
        }

        [HttpPost]
        [Route("api/security/login")]
        public HttpResponseMessage Login(LoginRequest model)
        {
            CheckCredentialResponse checkResp = _router.Query<CheckCredentialRequest, CheckCredentialResponse>(new CheckCredentialRequest()
            {
                Username = model.Username,
                Password = model.Password
            });

            if (checkResp.PrincipalId != default(Guid))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            if (!checkResp.Success)
            {
                _router.Push<LoginAttemptEvent>(evt =>
                {
                    evt.PrincipalId = checkResp.PrincipalId;
                    evt.Browser = Context.Client.UA.Family;
                    evt.System = Context.Client.OS.Family;
                    evt.Device = $"{Context.Client.Device.Family} {Context.Client.Device.Brand} {Context.Client.Device.Model}";
                    evt.RemoteAddress = Request.GetOwinContext().Request.RemoteIpAddress;
                });

                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            GenerateTokenResponse tokenResp = _router.Query<GenerateTokenRequest, GenerateTokenResponse>(new GenerateTokenRequest()
            {
                Key = Context.ServerConfig.AuthConfig.TokenKey,
                Algorithm = Context.ServerConfig.AuthConfig.TokenAlgorithm,
                Tokens = Context.ServerConfig.AuthConfig.TokenSpecs.Select(x => new Token()
                {
                    Type = x.Type,
                    Expiration = Context.Clock.UtcNow.Add(x.Duration).UtcDateTime,
                    PrincipalId = checkResp.PrincipalId
                })
            });

            return Request.CreateResponse(HttpStatusCode.OK, new LoginResponse()
            {
                Tokens = tokenResp.Tokens
            });
        }

        [HttpPost]
        [Route("api/security/register")]
        public HttpResponseMessage Register(RegisterRequest model)
        {
            Principal principal = new Principal()
            {
                Id = Guid.NewGuid(),
                Email = model.Email,
                Name = model.Username,
                Flags = new HashSet<PrincipalFlag> { PrincipalFlag.NeedsVerification },
                Kind = PrincipalKind.User,
                Password = Guid.NewGuid().ToString("N"),
                Created = Context.Clock.UtcNow.UtcDateTime,
                Metadata = new Dictionary<string, object>()
                {
                    { "registerIp", Request.GetOwinContext().Request.RemoteIpAddress },
                    { "registerUa", Context.Client.ToString() }
                }
            };

            try
            {
                _router.Command(new CreateCommand<Principal>()
                {
                    Object = principal
                });
            }
            catch (StorageException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict);
            }

            _router.Push<RegisteredPrincipalEvent>(evt => { evt.PrincipalId = principal.Id; });

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("api/security/refresh")]
        public HttpResponseMessage Refresh([FromHeader(Name="Authorization")] string tokenStr)
        {
            Token token = Token.Parse(tokenStr);

            DecodeTokenResponse decodeResp = _router.Query<DecodeTokenRequest, DecodeTokenResponse>(
                new DecodeTokenRequest()
                {
                    Key = Context.ServerConfig.AuthConfig.TokenKey,
                    Algorithm = Context.ServerConfig.AuthConfig.TokenAlgorithm,
                    Data = Convert.FromBase64String(token.Data),
                    IV = Convert.FromBase64String(token.InitValue)
                });

            if (decodeResp.Token.Type != TokenType.Bearer && decodeResp.Token.Type != TokenType.Refresh)
            {
                return Request.CreateResponse((HttpStatusCode)422, new {Errors = new[]{ "Authorization Token was not of expected types Bearer or Refresh!" }});
            }

            GenerateTokenResponse tokenResp = _router.Query<GenerateTokenRequest, GenerateTokenResponse>(new GenerateTokenRequest()
            {
                Key = Context.ServerConfig.AuthConfig.TokenKey,
                Algorithm = Context.ServerConfig.AuthConfig.TokenAlgorithm,
                Tokens = Context.ServerConfig.AuthConfig.TokenSpecs.Select(x => new Token()
                {
                    Type = x.Type,
                    Expiration = Context.Clock.UtcNow.Add(x.Duration).UtcDateTime,
                    PrincipalId = decodeResp.Token.PrincipalId
                })
            });

            return Request.CreateResponse(HttpStatusCode.OK, new LoginResponse()
            {
                Tokens = tokenResp.Tokens.Select(x => x.ToString())
            });
        }

        [HttpPost]
        [Route("api/security/forgot")]
        public HttpResponseMessage ForgotPassword([FromBody] string email)
        {
            GetResponse<Principal> principalResp = _router.Query<GetPrincipalByEmailRequest, GetResponse<Principal>>(new GetPrincipalByEmailRequest()
            {
                Email = email
            });

            if (principalResp.Result == null)
            {
                if (Context.ServerConfig.AuthConfig.IndicateBadResetEmail)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }

            _router.Command(new SendPasswordResetCommand()
            {
                PrincipalId = principalResp.Result.Id
            });

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("api/security/reset")]
        public HttpResponseMessage ResetPassword([FromUri] string token, [FromBody] string password)
        {
            Token resetToken = Token.Parse(token);

            DecodeTokenResponse decodeResp = _router.Query<DecodeTokenRequest, DecodeTokenResponse>(
                new DecodeTokenRequest()
                {
                    Key = Context.ServerConfig.AuthConfig.TokenKey,
                    Algorithm = Context.ServerConfig.AuthConfig.TokenAlgorithm,
                    Data = Convert.FromBase64String(resetToken.Data),
                    IV = Convert.FromBase64String(resetToken.InitValue)
                });

            if (Context.Clock.UtcNow > decodeResp.Token.Expiration)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            _router.Command(new SetPasswordCommand()
            {
                PrincipalId = resetToken.PrincipalId,
                Password = password
            });

            GenerateTokenResponse tokenResp = _router.Query<GenerateTokenRequest, GenerateTokenResponse>(new GenerateTokenRequest()
            {
                Key = Context.ServerConfig.AuthConfig.TokenKey,
                Algorithm = Context.ServerConfig.AuthConfig.TokenAlgorithm,
                Tokens = Context.ServerConfig.AuthConfig.TokenSpecs.Select(x => new Token()
                {
                    Type = x.Type,
                    Expiration = Context.Clock.UtcNow.Add(x.Duration).UtcDateTime
                })
            });

            return Request.CreateResponse(HttpStatusCode.OK, new LoginResponse()
            {
                Tokens = tokenResp.Tokens.Select(x => x.ToString())
            });
        }
    }
}
