using OpenSheets.Core;
using OpenSheets.Storage;

namespace OpenSheets.Services.Storage
{
    public class IdentityStorage
    {
        private readonly Storage<Identity> _storage;

        public IdentityStorage(Storage<Identity> storage)
        {
            _storage = storage;
        }
    }
}