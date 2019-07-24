using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace OpenSheets.Common
{
    public class ContextedController : ApiController
    {
        protected WebContext Context { get; }

        public ContextedController(WebContext context)
        {
            Context = context;
        }
    }
}
