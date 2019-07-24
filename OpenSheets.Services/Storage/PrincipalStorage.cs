using OpenSheets.Core;

namespace OpenSheets.Services.Storage
{
    public class PrincipalStorage
    {
        private readonly Storage<Principal> _storage;

        public PrincipalStorage(Storage<Principal> storage)
        {
            _storage = storage;
        }

        public Principal GetPrincipalByUsername(string username)
        {
            return _storage.Collection.Find(x => x.Name == username).FirstOrDefault();
        }
    }
}