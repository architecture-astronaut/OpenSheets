using System;
using System.Collections.Generic;

namespace OpenSheets.Storage
{
    public class StorageManagerConfigurer
    {
        private string _defaultDatabase;

        private Dictionary<Type, Tuple<string, string>> _typeMaps = new Dictionary<Type, Tuple<string, string>>();

        private object _lock = new object();

        private bool _finalized = false;

        internal StorageManagerConfigurer()
        {

        }

        public StorageManagerConfigurer DefaultDatabase(string database)
        {
            _defaultDatabase = database;

            return this;
        }

        public StorageManagerConfigurer Collection<T>(string database, string collection)
        {
            if (_finalized)
            {
                throw new StorageConfigurationException();
            }

            lock (_lock)
            {
                _typeMaps.Add(typeof(T), Tuple.Create(database, collection));
            }

            return this;
        }

        public StorageManagerConfigurer Collection<T>(string collection)
        {
            return Collection<T>(null, collection);
        }

        public StorageManagerConfigurer Collection<T>()
        {
            return Collection<T>(null, null);
        }

        public StorageManager LoadConfiguration()
        {
            throw new NotImplementedException();
        }

        public StorageManager Initialize()
        {
            Dictionary<Type, Tuple<string, string>> typeMaps = new Dictionary<Type, Tuple<string, string>>();

            foreach (var mapLine in _typeMaps)
            {
                if (!string.IsNullOrWhiteSpace(mapLine.Value.Item1) && !string.IsNullOrWhiteSpace(mapLine.Value.Item2))
                {
                    typeMaps.Add(mapLine.Key, mapLine.Value);

                    continue;
                }

                string db = mapLine.Value.Item1, collection = mapLine.Value.Item2;

                if (string.IsNullOrWhiteSpace(mapLine.Value.Item1))
                {
                    db = _defaultDatabase;
                }

                if (string.IsNullOrWhiteSpace(mapLine.Value.Item2))
                {
                    collection = mapLine.Key.Name.ToLower();
                }

                typeMaps.Add(mapLine.Key, Tuple.Create(db, collection));
            }

            return new StorageManager(typeMaps);
        }
    }
}