using System.Net.Http;
using System.Web.Http;
using OpenSheets.Common;
using OpenSheets.Core.Hexagon;

namespace OpenSheets.Auth.Controllers
{
    public class IdentityController : ContextedController
    {
        private readonly IServiceRouter _router;

        public IdentityController(WebContext context, IServiceRouter router) : base(context)
        {
            _router = router;
        }

        [HttpGet]
        [Route("api/identity/my")]
        public HttpResponseMessage GetIdentities()
        {

        }

        [HttpPut]
        [Route("api/identity/create")]
        public HttpResponseMessage CreateIdentity()
        {

        }

        [HttpGet]
        [Route("api/identity/{identityId}")]
        public HttpResponseMessage GetIdentityDetails()
        {

        }

        [HttpPost]
        [Route("api/identity/{identityId}/update")]
        public HttpResponseMessage UpdateIdentity()
        {

        }

        [HttpDelete]
        [Route("api/identity/{identityId}/delete")]
        public HttpResponseMessage DeleteIdentity()
        {

        }
    }
}