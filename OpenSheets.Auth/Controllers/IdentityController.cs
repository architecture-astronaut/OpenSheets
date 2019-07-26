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

            GetIdentitiesResponse response = _router.Query<GetIdentitiesByPrincipalRequest, GetIdentitiesResponse>(new GetIdentitiesByPrincipalRequest()
            {
                PrincipalId = Context.Principal.Id
            });

            if (response.Identities == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, response.Identities.Select(x => new
            {
                x.Id,
                x.Name,
                x.Kind
            }));
        }

        [HttpPut]
        [Route("api/identity/create")]
        public HttpResponseMessage CreateIdentity(Identity model)
        {
            return Request.CreateResponse(HttpStatusCode.NotImplemented);
        }

        [HttpGet]
        [Route("api/identity/{identityId}")]
        public HttpResponseMessage GetIdentityDetails(Guid identityId)
        {
            if (identityId == Guid.Empty)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            GetIdentityResponse response = _router.Query<GetIdentityByIdRequest, GetIdentityResponse>(new GetIdentityByIdRequest()
            {
                IdentityId = identityId
            });

            if (response.Identity == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, response.Identity);
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
        public HttpResponseMessage PatchIdentity(Guid identityId, Guid version, [FromBody] JsonPatchDocument<Identity> model)
        {
            if (identityId == Guid.Empty)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            
            GetIdentityResponse response = _router.Query<GetIdentityByIdRequest, GetIdentityResponse>(new GetIdentityByIdRequest()
            {
                IdentityId = identityId
            });
            
            if (response.Identity == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (response.Identity.Version != version)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict);
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

            return Request.CreateResponse(HttpStatusCode.NotImplemented);
        }
    }
}