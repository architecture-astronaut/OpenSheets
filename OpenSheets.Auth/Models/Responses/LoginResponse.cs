using System.Collections.Generic;

namespace OpenSheets.Auth.Responses
{
    public class LoginResponse
    {
        public IEnumerable<string> Tokens { get; set; }
    }
}