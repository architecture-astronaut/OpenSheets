using System;
using System.Collections.Generic;

namespace OpenSheets.Core
{
    public class Membership
    {
        public Guid Id { get; set; }
        public Guid IdentityId { get; set; }
        public Guid OrganizationId { get; set; }
        public HashSet<MembershipFlag> Flags { get; set; }
        public HashSet<Guid> Roles { get; set; }
        public MembershipKind Kind { get; set; }
        public Dictionary<string, object> Metadata { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}