using System.Linq;
using OpenSheets.Contracts.Commands;
using OpenSheets.Core;
using OpenSheets.Core.Hexagon;
using OpenSheets.Services.Storage;

namespace OpenSheets.Services.Handlers
{
    public class PatchIdentityHandler : HandleCommand<PatchCommand<Identity>>
    {
        private readonly IdentityStorage _storage;

        public PatchIdentityHandler(IdentityStorage storage)
        {
            _storage = storage;
        }

        public override void Command(PatchCommand<Identity> request, IServiceRouter router, RequestContext context)
        {
            Identity identity = _storage.GetIdentityById(request.IdentityId);

            request.Patch.ApplyTo(identity);

            _storage.SaveIdentity(identity, request.NewVersion);

            router.Push<IdentityUpdatedEvent>(evt =>
            {
                evt.IdentityId = identity.Id;
                evt.NewVersion = request.NewVersion;
                evt.OldVersion = identity.Version;
            });
        }
    }
}