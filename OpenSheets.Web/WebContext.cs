using System.Web;
using Microsoft.Owin;
using Microsoft.Owin.Infrastructure;
using OpenSheets.Core;
using UAParser;

namespace OpenSheets.Web
{
    public class WebContext : IWebContext
    {
        private readonly IOwinContext _owinContext;

        public WebContext(IOwinContext owinContext, ISystemClock clock, ServerConfiguration serverConfig)
        {
            _owinContext = owinContext;
            Clock = clock;
            ServerConfig = serverConfig;
        }

        public ISystemClock Clock { get; }
        public ServerConfiguration ServerConfig { get; }

        public ClientInfo Client
        {
            get
            {
                return Parser.GetDefault().Parse(_owinContext.Request.Headers["User-Agent"]);
            }
        }

        public Principal Principal {
            get
            {
                return _owinContext.Get<Principal>("openSheets.principal");
            }
        }

        public Identity Identity
        {
            get
            {
                return _owinContext.Get<Identity>("openSheets.identity");
            }
        }
    }
}