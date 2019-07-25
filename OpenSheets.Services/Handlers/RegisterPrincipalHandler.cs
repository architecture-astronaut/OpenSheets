using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSheets.Auth.Commands;
using OpenSheets.Contracts.Events;
using OpenSheets.Core;
using OpenSheets.Core.Hexagon;
using OpenSheets.Core.Utilities;
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

            router.Command(new PushEvent()
            {
                Event = new PrincipalCreatedEvent()
                {
                    PrincipalId = request.Principal.Id
                }
            });
        }
    }

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

            router.Command(new PushEvent()
            {
                Event = new PasswordSetEvent()
                {
                    PrincipalId = request.PrincipalId
                }
            });
        }
    }

    public class CreateIdentityHandler : HandleCommand<CreateIdentityCommand>
    {
        public override void Command(CreateIdentityCommand request, IServiceRouter router, RequestContext context)
        {
            throw new NotImplementedException();
        }
    }
}
