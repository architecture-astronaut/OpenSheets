using System.Collections.Generic;
using OpenSheets.Common;

namespace OpenSheets.Contracts.Requests
{
    public class GenerateTokenRequest
    {
        public string Algorithm { get; set; }
        public byte[] Key { get; set; }
        public IEnumerable<Token> Tokens { get; set; }
    }
}