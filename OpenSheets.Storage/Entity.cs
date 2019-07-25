using System;

namespace OpenSheets.Storage
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int Version { get; set; }
    }
}