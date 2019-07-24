using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OpenSheets.Auth.Commands;
using OpenSheets.Auth.Events;
using OpenSheets.Auth.Requests;
using OpenSheets.Auth.Responses;
using OpenSheets.Common;
using OpenSheets.Core;
using OpenSheets.Core.Hexagon;
using OpenSheets.Storage;

namespace OpenSheets.Auth.Controllers
{
    public class SecurityController : ContextedController
    {
        private readonly IServiceRouter _router;

        public SecurityController(WebContext context, IServiceRouter router) : base(context)
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
                _router.Command(new PushEvent()
                {
                    Event = new LoginAttemptEvent()
                    {
                        PrincipalId = checkResp.PrincipalId,
                        Browser = Context.Client.UA.Family,
                        System = Context.Client.OS.Family,
                        Device = $"{Context.Client.Device.Family} {Context.Client.Device.Brand} {Context.Client.Device.Model}",
                        RemoteAddress = Request.GetOwinContext().Request.RemoteIpAddress
                    }
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
                    Expiration = Context.Clock.UtcNow.Add(x.Duration).UtcDateTime
                })
            });

            return Request.CreateResponse(HttpStatusCode.OK, new LoginResponse()
            {
                Tokens = tokenResp.Tokens.Select(x => x.ToString())
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
                _router.Command(new RegisterPrincipalCommand()
                {
                    Principal = principal
                });
            }
            catch (StorageException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("api/security/forgot")]
        public HttpResponseMessage ForgotPassword([FromBody] string email)
        {
            GetPrincipalResponse principalResp = _router.Query<GetPrincipalByEmailRequest, GetPrincipalResponse>(new GetPrincipalByEmailRequest()
            {
                Email = email
            });

            if (principalResp.Principal == null)
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
                PrincipalId = principalResp.Principal.Id
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
