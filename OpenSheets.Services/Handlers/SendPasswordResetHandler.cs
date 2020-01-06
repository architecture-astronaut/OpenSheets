using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using OpenSheets.Contracts.Commands;
using OpenSheets.Contracts.Commands.Communication;
using OpenSheets.Contracts.Requests;
using OpenSheets.Contracts.Responses;
using OpenSheets.Core;
using OpenSheets.Core.Configuration;
using OpenSheets.Core.Hexagon;
using OpenSheets.Service.Comm;

namespace OpenSheets.Services.Handlers
{
    public class SendPasswordResetHandler : HandleCommand<SendPasswordResetCommand>
    {
        private readonly ISendEndpoint _endpoint;
        private readonly PasswordManagementConfiguration _passwordManagementConfiguration;

        public SendPasswordResetHandler(ISendEndpoint endpoint, PasswordManagementConfiguration passwordManagementConfiguration)
        {
            _endpoint = endpoint;
            _passwordManagementConfiguration = passwordManagementConfiguration;
        }

        public override void Command(SendPasswordResetCommand request, IServiceRouter router, RequestContext context)
        {
            GetResponse<Principal> principalResponse = router.Query<GetPrincipalByIdRequest, GetResponse<Principal>>(new GetPrincipalByIdRequest() { PrincipalId = request.PrincipalId });

            GenerateTokenResponse tokenResponse = router.Query<GenerateTokenRequest, GenerateTokenResponse>(
                new GenerateTokenRequest()
                {
                    
                });

            router.Command(new TemplateEmailMessage()
            {
                From = _passwordManagementConfiguration.OriginationAddress,
                TemplateId = _passwordManagementConfiguration.TemplateName,
                To = new []{ principalResponse.Result.Email },
                Id = Guid.NewGuid(),
                TemplateData = new Dictionary<string, object>
                {
                    { "reset_token", tokenResponse.Tokens.Single().ToString() }
                }
            });
        }
    }

    [ConfigurationClass("Security.Password")]
    public class PasswordManagementConfiguration
    {
        [ConfigurationItem("OriginationAddress")]
        public string OriginationAddress { get; set; }

        [ConfigurationItem("Template")]
        public string TemplateName { get; set; }
    }
}
