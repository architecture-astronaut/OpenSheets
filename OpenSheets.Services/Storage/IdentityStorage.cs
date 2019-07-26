using System;
using MongoDB.Driver;
using NodaTime;
using OpenSheets.Core;
using OpenSheets.Storage;

namespace OpenSheets.Services.Storage
{
    public class IdentityStorage
    {
        private readonly Storage<Identity> _storage;
        private readonly IClock _clock;

        public IdentityStorage(Storage<Identity> storage, IClock clock)
        {
            _storage = storage;
            _clock = clock;
        }

        public Identity GetIdentityById(Guid identityId)
        {
            return _storage.Collection.Find(x => x.Id == identityId).FirstOrDefault();
        }

        public void CreateIdentity(Identity identity)
        {
            _storage.Collection.InsertOne(identity);
        }

        public void SaveIdentity(Identity identity, Guid? newVersion)
        {
            Identity currentIdentity = GetIdentityById(identity.Id);

            if (currentIdentity.Version != identity.Version)
            {
                throw new StorageException();
            }

            identity.Updated = _clock.GetCurrentInstant().ToDateTimeUtc();

            if (newVersion == null)
            {
                identity.Version = Guid.NewGuid();
            }
            else
            {
                identity.Version = newVersion.Value;
            }

            _storage.Collection.ReplaceOne(p => p.Id == identity.Id, identity);
            _storage.Historical.InsertOne(currentIdentity);
        }
    }
}