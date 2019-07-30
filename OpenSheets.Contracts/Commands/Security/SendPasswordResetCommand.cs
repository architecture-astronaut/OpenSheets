using System;

namespace OpenSheets.Contracts.Commands
{
    public class SendPasswordResetCommand
    {
        public Guid PrincipalId { get; set; }
    }
}