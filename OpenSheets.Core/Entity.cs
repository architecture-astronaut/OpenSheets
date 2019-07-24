using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace OpenSheets.Core
{
    public class Entity
    {
        public Guid Id { get; set; }
        public Guid FileId { get; set; }
        public Guid CreatorId { get; set; }
        public Guid OwnerId { get; set; }
        public IEnumerable<Link> Links { get; set; }
        public HashSet<EntityFlag> Flags { get; set; }
        public string Name { get; set; }
        public EntityType Type { get; set; }
        public JObject Data { get; set; }
        public string Version { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}