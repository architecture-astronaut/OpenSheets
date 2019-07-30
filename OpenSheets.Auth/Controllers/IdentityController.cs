using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Marvin.JsonPatch;
using OpenSheets.Common;
using OpenSheets.Contracts.Commands;
using OpenSheets.Contracts.Requests;
using OpenSheets.Core;
using OpenSheets.Core.Hexagon;
using OpenSheets.Web;

namespace OpenSheets.Auth.Controllers
{
    public class IdentityController : ContextedController
    {
        private readonly IServiceRouter _router;

        public IdentityController(IWebContext context, IServiceRouter router) : base(context)
        {
            _router = router;
        }

        [HttpGet]
        [Route("api/identity/my")]
        public HttpResponseMessage GetIdentities()
        {
            if (Context.Principal == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            GetCollectionResponse<Identity> response = _router.Query<GetIdentitiesByPrincipalRequest, GetCollectionResponse<Identity>>(new GetIdentitiesByPrincipalRequest()
            {
                PrincipalId = Context.Principal.Id
            });

            if (response.Results == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, response.Results.Select(x => new
            {
                x.Id,
                x.Name,
                x.Kind
            }));
        }

        [HttpPut]
        [Route("api/identity/create")]
        public HttpResponseMessage CreateIdentity([FromBody]Identity model, [FromHeader(Name = "opensheets-bypass-level")] Level bypassLevel = Level.Information)
        {
            if (model.PrincipalId != Context.Principal.Id && !Context.Identity.Flags.Contains(IdentityFlag.SysAdmin))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            ValidateResponse validateResp = _router.Query<ValidateRequest<Identity>, ValidateResponse>(
                new ValidateRequest<Identity>()
                {
                    ObjectId = Guid.Empty,
                    Object = model
                });
            
            if (validateResp.Results.Any(x => x.Level > Level.Information))
            {
                return Request.CreateResponse((HttpStatusCode)422, new { Validation = new { Errors = validateResp.Results } });
            }

            model.Id = Guid.NewGuid();

            _router.Command(new CreateCommand<Identity>()
            {
                Object = model
            });

            return Request.CreateResponse(HttpStatusCode.OK, new {Id = model.Id});
        }

        [HttpGet]
        [Route("api/identity/{identityId}")]
        public HttpResponseMessage GetIdentityDetails(Guid identityId)
        {
            if (identityId == Guid.Empty)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            GetResponse<Identity> response = _router.Query<GetIdentityByIdRequest, GetResponse<Identity>>(new GetIdentityByIdRequest()
            {
                IdentityId = identityId
            });

            if (response.Result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, response.Result);
        }

        //[HttpPost]
        //[Route("api/identity/{identityId}/update")]
        //public HttpResponseMessage UpdateIdentity(Guid identityId, Identity model)
        //{
        //    if (identityId == Guid.Empty)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest);
        //    }

        //    _router.Command(new );
        //}

        [HttpPatch]
        [Route("api/identity/{identityId}/patch/{version}")]
        public HttpResponseMessage PatchIdentity(Guid identityId, Guid version, [FromBody] JsonPatchDocument<Identity> model, [FromUri] Level bypassLevel = Level.Information)
        {
            if (bypassLevel > Level.Warning && (Level) Context.Principal.Metadata["Allowed-Bypass"] < bypassLevel)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, new {Reason = $"Attempted to bypass validation of {bypassLevel} level, only allowed { (Level?)Context.Principal.Metadata["Allowed-Bypass"] ?? Level.Warning }"});
            }

            if (identityId == Guid.Empty)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            GetResponse<Identity> response = _router.Query<GetIdentityByIdRequest, GetResponse<Identity>>(new GetIdentityByIdRequest()
            {
                IdentityId = identityId
            });
            
            if (response.Result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (response.Result.Version != version)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict);
            }

            ValidatePatchResponse validateResp = _router.Query<ValidatePatchRequest<Identity>, ValidatePatchResponse>(new ValidatePatchRequest<Identity>()
            {
                ObjectId = identityId,
                ProposedPatch = model
            });

            if (validateResp.Results.Any(x => x.Level > bypassLevel))
            {
                return Request.CreateResponse((HttpStatusCode) 422, new { Validation = new { Errors = validateResp.Results } });
            }

            PatchCommand<Identity> request = new PatchCommand<Identity>()
            {
                NewVersion = Guid.NewGuid(),
                Patch = model
            };

            _router.Command(request);

            return Request.CreateResponse(HttpStatusCode.OK, new {Version = request.NewVersion});
        }

        [HttpDelete]
        [Route("api/identity/{identityId}/delete")]
        public HttpResponseMessage DeleteIdentity(Guid identityId)
        {
            if (identityId == Guid.Empty)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            GetResponse<Identity> response = _router.Query<GetIdentityByIdRequest, GetResponse<Identity>>(new GetIdentityByIdRequest()
            {
                IdentityId = identityId
            });

            if (response.Result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (Context.Principal.Id != response.Result.PrincipalId && !Context.Identity.Flags.Contains(IdentityFlag.SysAdmin))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            _router.Command(new RemoveCommand<Identity>()
            {
                ObjectId = identityId,
                Object = response.Result
            });

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}