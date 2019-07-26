using System;

namespace OpenSheets.Contracts.Requests
{
    public class GetIdentitiesByPrincipalRequest
    {
        public Guid PrincipalId { get; set; }
    }
}