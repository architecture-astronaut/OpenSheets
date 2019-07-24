using System.Collections.Generic;

namespace OpenSheets.Contracts.Responses
{
    public class GenerateTokenResponse
    {
        public IEnumerable<Token> Tokens { get; set; }
    }
}