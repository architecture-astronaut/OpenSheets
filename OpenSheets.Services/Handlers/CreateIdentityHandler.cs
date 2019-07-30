using System;
using OpenSheets.Contracts.Commands;
using OpenSheets.Core;
using OpenSheets.Core.Hexagon;

namespace OpenSheets.Services.Handlers
{
    public class CreateIdentityHandler : HandleCommand<CreateCommand<Identity>>
    {
        public override void Command(CreateCommand<Identity> request, IServiceRouter router, RequestContext context)
        {
            throw new NotImplementedException();
        }
    }
}