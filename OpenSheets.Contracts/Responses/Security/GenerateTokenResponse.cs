using System.Collections.Generic;
using OpenSheets.Common;

namespace OpenSheets.Contracts.Responses
{
    public class GenerateTokenResponse
    {
        public IEnumerable<Token> Tokens { get; set; }
    }
}