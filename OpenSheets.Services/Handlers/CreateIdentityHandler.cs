using System;
using OpenSheets.Contracts.Commands;
using OpenSheets.Core.Hexagon;

namespace OpenSheets.Services.Handlers
{
    public class CreateIdentityHandler : HandleCommand<CreateIdentityCommand>
    {
        public override void Command(CreateIdentityCommand request, IServiceRouter router, RequestContext context)
        {
            throw new NotImplementedException();
        }
    }
}