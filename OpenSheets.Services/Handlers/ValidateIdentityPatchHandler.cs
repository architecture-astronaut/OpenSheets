using System.Linq;
using OpenSheets.Contracts.Commands;
using OpenSheets.Core;
using OpenSheets.Core.Hexagon;

namespace OpenSheets.Services.Handlers
{
    public class ValidateIdentityPatchHandler : HandleQuery<ValidatePatchRequest<Identity>, ValidatePatchResponse>
    {
        public override ValidatePatchResponse Query(ValidatePatchRequest<Identity> request, IServiceRouter router, RequestContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}