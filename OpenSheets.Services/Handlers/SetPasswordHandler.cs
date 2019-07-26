using OpenSheets.Contracts.Commands;
using OpenSheets.Contracts.Events;
using OpenSheets.Core;
using OpenSheets.Core.Hexagon;
using OpenSheets.Core.Utilities;
using OpenSheets.Services.Storage;

namespace OpenSheets.Services.Handlers
{
    public class SetPasswordHandler : HandleCommand<SetPasswordCommand>
    {
        private readonly PrincipalStorage _storage;

        public SetPasswordHandler(PrincipalStorage storage)
        {
            _storage = storage;
        }

        public override void Command(SetPasswordCommand request, IServiceRouter router, RequestContext context)
        {
            Principal principal = _storage.GetPrincipalById(request.PrincipalId);

            principal.Password = Password.CreateHash(request.Password);

            _storage.SavePrincipal(principal);

            router.Push<PasswordSetEvent>(evt => { evt.PrincipalId = request.PrincipalId; });
        }
    }
}