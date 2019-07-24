using OpenSheets.Core;
using OpenSheets.Core.Hexagon;
using OpenSheets.Core.Utilities;
using OpenSheets.Services.Storage;

namespace OpenSheets.Services.Handlers
{
    public class CheckCredentialHandler : HandleQuery<CheckCredentialRequest,CheckCredentialResponse>
    {
        private readonly PrincipalStorage _principalStorage;

        public CheckCredentialHandler(PrincipalStorage principalStorage)
        {
            _principalStorage = principalStorage;
        }

        public override CheckCredentialResponse Query(CheckCredentialRequest request, IServiceRouter router, RequestContext context)
        {
            CheckCredentialResponse response = new CheckCredentialResponse();

            Principal principal = _principalStorage.GetPrincipalByUsername(request.Username);

            if (principal == null)
            {
                return response;
            }

            response.PrincipalId = principal.Id;

            if (Password.VerifyPassword(request.Password, principal.Password))
            {
                response.Success = true;
            }

            return response;
        }
    }
}