using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using OpenSheets.Common;
using OpenSheets.Contracts.Requests;
using OpenSheets.Contracts.Responses;
using OpenSheets.Core.Hexagon;

namespace OpenSheets.Services.Handlers
{
    public class DecodeTokenHandler : HandleQuery<DecodeTokenRequest, DecodeTokenResponse>
    {
        public override DecodeTokenResponse Query(DecodeTokenRequest request, IServiceRouter router, RequestContext context)
        {
            DecodeTokenResponse response = new DecodeTokenResponse();

            DecryptResponse decrypt = router.Query<DecryptRequest, DecryptResponse>(new DecryptRequest()
            {
                Key = request.Key,
                CipherText = request.Data,
                IV = request.IV,
                Algorithm = request.Algorithm
            });

            string tokenStr = Encoding.ASCII.GetString(decrypt.ClearText);

            Dictionary<string, string> parts = tokenStr.Split('|').Select(x => x.Split(new[] {':'}, 1)).ToDictionary(x => x[0], x => x[1]);

            Token token = new Token()
            {
                Type = (TokenType)Enum.Parse(typeof(TokenType), parts["type"], true),
                PrincipalId = Guid.Parse(parts["prin"]),
                Data = Convert.ToBase64String(request.Data),
                Expiration = DateTime.ParseExact(parts["expire"], "yyyyMMddHHmmss", CultureInfo.CurrentCulture),
                Issued = DateTime.ParseExact(parts["issued"], "yyyyMMddHHmmss", CultureInfo.CurrentCulture)
            };

            response.Token = token;

            return response;
        }
    }
}