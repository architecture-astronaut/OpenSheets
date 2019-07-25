using System.Threading.Tasks;
using Microsoft.Owin;
using MongoDB.Driver;

namespace OpenSheets.Storage.Middleware
{
    public class SessionMiddleware : OwinMiddleware
    {
        private readonly OwinMiddleware _next;
        private readonly IClientSession _session;

        public SessionMiddleware(OwinMiddleware next, IClientSession session) : base(next)
        {
            _next = next;
            _session = session;
        }

        public override async Task Invoke(IOwinContext context)
        {
            _session.StartTransaction();

            await _next.Invoke(context);

            _session.CommitTransaction();
        }
    }
}