using System.Collections.Generic;
using OpenSheets.Core;

namespace OpenSheets.Auth.Controllers
{
    public class GetIdentitiesResponse
    {
        public IEnumerable<Identity> Identities { get; set; }
    }
}