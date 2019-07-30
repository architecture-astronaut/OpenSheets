using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSheets.Contracts.Commands;
using OpenSheets.Contracts.Events;
using OpenSheets.Core;
using OpenSheets.Core.Hexagon;
using OpenSheets.Services.Storage;

namespace OpenSheets.Services.Handlers
{
    public class RegisterPrincipalHandler : HandleCommand<CreateCommand<Principal>>
    {
        private readonly PrincipalStorage _storage;

        public RegisterPrincipalHandler(PrincipalStorage storage)
        {
            _storage = storage;
        }

        public override void Command(CreateCommand<Principal> request, IServiceRouter router, RequestContext context)
        {
            _storage.CreatePrincipal(request.Object);

            router.Push<PrincipalCreatedEvent>(evt => { evt.PrincipalId = request.Object.Id; });
        }
    }
}
