using System;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace OpenSheets.Auth.Middlewares
{
    public class AuthenticationMiddleware : OwinMiddleware
    {
        public AuthenticationMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            throw new NotImplementedException();
        }
    }
}