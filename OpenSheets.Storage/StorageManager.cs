using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace OpenSheets.Storage
{
    public class StorageManager
    {
        private readonly Dictionary<Type, Tuple<string, string>> _typeMaps = new Dictionary<Type, Tuple<string, string>>();

        internal StorageManager(Dictionary<Type, Tuple<string, string>> maps)
        {
            _typeMaps = maps;
        }

        public string GetDatabase<T>()
        {
            return _typeMaps[typeof(T)]?.Item1 ?? throw new StorageConfigurationException();
        }

        public string GetCollection<T>()
        {
            return _typeMaps[typeof(T)].Item2 ?? throw new StorageConfigurationException();
        }

        public static StorageManagerConfigurer Configure()
        {
            return _configurer;
        }

        private static StorageManagerConfigurer _configurer = new StorageManagerConfigurer();

        public Storage<T> CreateStorage<T>(IClientSession session)
        {
            return new Storage<T>(session, _typeMaps[typeof(T)].Item1, _typeMaps[typeof(T)].Item2);
        }
    }
}