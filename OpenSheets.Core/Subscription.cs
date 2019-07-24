using System;
using System.Collections.Generic;

namespace OpenSheets.Core
{
    public class Subscription
    {
        public Guid Id { get; set; }
        public Guid PrincipalId { get; set; }
        public Guid PlanId { get; set; }
        public HashSet<SubscriptionFlag> Flags { get; set; }
        public Dictionary<string, object> Metadata { get; set; }
        public Dictionary<string, object> Overrides { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}