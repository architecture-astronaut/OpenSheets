using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSheets.Contracts.Events
{
    public class PrincipalCreatedEvent
    {
        public Guid PrincipalId { get; set; }
    }
}
