using System;

namespace OpenSheets.Contracts.Events
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
