using System.Net;
using System.Net.Http;
using System.Web.Http;
using OpenSheets.Common;
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
            return Request.CreateResponse(HttpStatusCode.NotImplemented);
        }

        [HttpPut]
        [Route("api/identity/create")]
        public HttpResponseMessage CreateIdentity()
        {
            return Request.CreateResponse(HttpStatusCode.NotImplemented);
        }

        [HttpGet]
        [Route("api/identity/{identityId}")]
        public HttpResponseMessage GetIdentityDetails()
        {
            return Request.CreateResponse(HttpStatusCode.NotImplemented);
        }

        [HttpPost]
        [Route("api/identity/{identityId}/update")]
        public HttpResponseMessage UpdateIdentity()
        {
            return Request.CreateResponse(HttpStatusCode.NotImplemented);
        }

        [HttpDelete]
        [Route("api/identity/{identityId}/delete")]
        public HttpResponseMessage DeleteIdentity()
        {
            return Request.CreateResponse(HttpStatusCode.NotImplemented);
        }
    }
}