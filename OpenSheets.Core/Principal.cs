using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSheets.Core
{
    public class Principal
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public HashSet<PrincipalFlag> Flags { get; set; }
        public PrincipalKind Kind { get; set; }
        public Dictionary<string, object> Metadata { get; set; }
        public Guid Version { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
