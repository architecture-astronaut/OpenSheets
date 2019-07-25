using System;
using OpenSheets.Common;

namespace OpenSheets.Web
{
    public class TokenSpec
    {
        public TokenType Type { get; set; }
        public TimeSpan Duration { get; set; }
    }
}