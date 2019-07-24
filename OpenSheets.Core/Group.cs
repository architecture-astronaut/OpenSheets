using System;
using System.Collections.Generic;

namespace OpenSheets.Core
{
    public class Group
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public Guid OrganizationId { get; set; }
        public string Name { get; set; }
        public HashSet<GroupFlag> Flags { get; set; }
        public Dictionary<string, object> Metadata { get; set; }
        public GroupKind Kind { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}