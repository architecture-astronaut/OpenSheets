using System;
using System.Collections.Generic;

namespace OpenSheets.Core
{
    public class Authorization
    {
        public Guid Id { get; set; }
        public Guid LicenseId { get; set; }
        public Guid PrincipalId { get; set; }
        public AuthorizationKind Kind { get; set; }
        public Dictionary<string, object> Metadata { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}