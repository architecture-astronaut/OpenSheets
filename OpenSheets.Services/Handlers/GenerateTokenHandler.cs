using System;
using System.Collections.Generic;
using System.Text;
using OpenSheets.Core.Hexagon;

namespace OpenSheets.Services.Handlers
{
    public class GenerateTokenHandler : HandleQuery<GenerateTokenRequest, GenerateTokenResponse>
    {
        private readonly IClock _clock;

        public GenerateTokenHandler(IClock clock)
        {
            _clock = clock;
        }

        public override GenerateTokenResponse Query(GenerateTokenRequest request, IServiceRouter router, RequestContext context)
        {
            List<Token> tokens = new List<Token>();

            foreach (var token in request.Tokens)
            {
                DateTime issued = _clock.GetCurrentInstant().ToDateTimeUtc();

                string composite = $"prin:{token.PrincipalId : N}|issued:{issued : yyyyMMddHHmmss}|expire:{token.Expiration: yyyyMMddHHmmss}|type:{token.Type : G}";

                byte[] bytes = Encoding.ASCII.GetBytes(composite);

                EncryptResponse response = router.Query<EncryptRequest, EncryptResponse>(new EncryptRequest()
                {
                    Algorithm = request.Algorithm,
                    ClearText = bytes,
                    Key = request.Key
                });

                Token newToken = new Token()
                {
                    Data = Convert.ToBase64String(response.CipherText),
                    Type = token.Type,
                    Expiration = token.Expiration,
                    Issued = issued,
                    PrincipalId = token.PrincipalId
                };

                tokens.Add(newToken);
            }

            return new GenerateTokenResponse()
            {
                Tokens = tokens
            };
        }
    }
}