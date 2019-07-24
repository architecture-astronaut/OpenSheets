using System;

namespace OpenSheets.Contracts.Responses
{
    public class CheckCredentialResponse
    {
        public Guid PrincipalId { get; set; }
        public bool Success { get; set; }
    }
}