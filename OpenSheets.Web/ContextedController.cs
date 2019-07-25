using System.Web.Http;

namespace OpenSheets.Web
{
    public class ContextedController : ApiController
    {
        protected IWebContext Context { get; }

        public ContextedController(IWebContext context)
        {
            Context = context;
        }
    }
}
