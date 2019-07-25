using System;

namespace OpenSheets.Auth.Commands
{
    public class SetPasswordCommand
    {
        public Guid PrincipalId { get; set; }
        public string Password { get; set; }
    }
}