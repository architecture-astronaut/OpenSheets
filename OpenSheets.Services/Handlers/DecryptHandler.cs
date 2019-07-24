using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using OpenSheets.Core.Hexagon;

namespace OpenSheets.Services.Handlers
{
    public class DecryptHandler : HandleQuery<DecryptRequest, DecryptResponse>
    {
        public override DecryptResponse Query(DecryptRequest request, IServiceRouter router, RequestContext context)
        {
            DecryptResponse response = new DecryptResponse();

            using (SymmetricAlgorithm alg = SymmetricAlgorithm.Create(request.Algorithm))
            {
                alg.IV = request.IV;

                alg.Key = request.Key;

                using (ICryptoTransform transform = alg.CreateEncryptor())
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        memoryStream.Write(request.CipherText, 0, request.CipherText.Length);

                        memoryStream.Flush();

                        List<byte> bytes = new List<byte>();
                        
                        using (CryptoStream stream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read))
                        {
                            int readByte;

                            while ((readByte = stream.ReadByte()) >= 0)
                            {
                                bytes.Add((byte)readByte);
                            }
                        }

                        response.ClearText = bytes.ToArray();
                    }
                }
            }

            return response;
        }
    }
}