using System;
using System.Collections.Generic;

namespace OpenSheets.Core
{
    public class Identity
    {
        public Guid Id { get; set; }
        public Guid PrincipalId { get; set; }
        public string Name { get; set; }
        public HashSet<IdentityFlag> Flags { get; set; }
        public Dictionary<string, object> Metadata { get; set; }
        public IdentityKind Kind { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}