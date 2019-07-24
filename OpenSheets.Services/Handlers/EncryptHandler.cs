using System.IO;
using System.Security.Cryptography;
using OpenSheets.Core.Hexagon;

namespace OpenSheets.Services.Handlers
{
    public class EncryptHandler : HandleQuery<EncryptRequest, EncryptResponse>
    {
        public override EncryptResponse Query(EncryptRequest request, IServiceRouter router, RequestContext context)
        {
            EncryptResponse response = new EncryptResponse();

            using (SymmetricAlgorithm alg = SymmetricAlgorithm.Create(request.Algorithm))
            {
                if (request.IV != null)
                {
                    alg.IV = request.IV;
                }
                else
                {
                    alg.GenerateIV();

                    response.IV = alg.IV;
                }

                alg.Key = request.Key;

                using (ICryptoTransform transform = alg.CreateEncryptor())
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream stream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
                        {
                            stream.Write(request.ClearText, 0, request.ClearText.Length);
                            
                            stream.Flush();
                        }

                        response.CipherText = memoryStream.ToArray();
                    }
                }
            }

            return response;
        }
    }
}
