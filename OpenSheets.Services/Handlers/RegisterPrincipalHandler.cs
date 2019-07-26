using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSheets.Contracts.Commands;
using OpenSheets.Contracts.Events;
using OpenSheets.Core.Hexagon;
using OpenSheets.Services.Storage;

namespace OpenSheets.Services.Handlers
{
    public class RegisterPrincipalHandler : HandleCommand<RegisterPrincipalCommand>
    {
        private readonly PrincipalStorage _storage;

        public RegisterPrincipalHandler(PrincipalStorage storage)
        {
            _storage = storage;
        }

        public override void Command(RegisterPrincipalCommand request, IServiceRouter router, RequestContext context)
        {
            _storage.CreatePrincipal(request.Principal);

            router.Push<PrincipalCreatedEvent>(evt => { evt.PrincipalId = request.Principal.Id; });
        }
    }
}
