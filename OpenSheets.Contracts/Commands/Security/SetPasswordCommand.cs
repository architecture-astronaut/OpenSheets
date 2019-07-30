using System;

namespace OpenSheets.Contracts.Commands
{
    public class SetPasswordCommand
    {
        public Guid PrincipalId { get; set; }
        public string Password { get; set; }
    }
}