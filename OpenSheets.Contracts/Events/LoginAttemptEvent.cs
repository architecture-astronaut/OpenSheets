using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OpenSheets.Auth.Events
{
    public class LoginAttemptEvent
    {
        public Guid PrincipalId { get; set; }
        public string RemoteAddress { get; set; }
        public string Browser { get; set; }
        public string System { get; set; }
        public string Device { get; set; }
    }
}
