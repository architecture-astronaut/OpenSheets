using System.Collections.Generic;

namespace OpenSheets.Web
{
    public class AuthenticationConfiguration
    {
        public byte[] TokenKey { get; set; }
        public string TokenAlgorithm { get; set; }
        public IEnumerable<TokenSpec> TokenSpecs { get; set; }
        public bool IndicateBadResetEmail { get; set; }
    }
}