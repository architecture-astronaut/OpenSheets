using System;
using MongoDB.Driver;
using NodaTime;
using OpenSheets.Core;
using OpenSheets.Storage;

namespace OpenSheets.Services.Storage
{
    public class PrincipalStorage
    {
        private readonly Storage<Principal> _storage;
        private readonly IClock _clock;

        public PrincipalStorage(Storage<Principal> storage, IClock clock)
        {
            _storage = storage;
            _clock = clock;
        }

        public Principal GetPrincipalByUsername(string username)
        {
            return _storage.Collection.Find(x => x.Name == username).FirstOrDefault();
        }

        public void CreatePrincipal(Principal principal)
        {
            _storage.Collection.InsertOne(principal);
        }

        public Principal GetPrincipalById(Guid principalId)
        {
            return _storage.Collection.Find(x => x.Id == principalId).FirstOrDefault();
        }

        public void SavePrincipal(Principal principal, Guid? newVersion = null)
        {
            Principal currentPrincipal = GetPrincipalById(principal.Id);

            if (currentPrincipal.Version != principal.Version)
            {
                throw new StorageException();
            }

            principal.Updated = _clock.GetCurrentInstant().ToDateTimeUtc();

            if (newVersion == null)
            {
                principal.Version = Guid.NewGuid();
            }
            else
            {
                principal.Version = newVersion.Value;
            }

            _storage.Collection.ReplaceOne(p => p.Id == principal.Id, principal);
            _storage.Historical.InsertOne(currentPrincipal);
        }
    }
}