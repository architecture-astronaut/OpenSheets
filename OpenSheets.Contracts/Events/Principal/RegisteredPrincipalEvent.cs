using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSheets.Contracts.Events.Principal
{
    public interface RegisteredPrincipalEvent
    {
        Guid PrincipalId { get; set; }
    }
}
