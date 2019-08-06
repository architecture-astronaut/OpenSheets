using System.Collections.Generic;
using OpenSheets.Common;

namespace OpenSheets.Auth.Responses
{
    public class LoginResponse
    {
        public IEnumerable<Token> Tokens { get; set; }
    }
}