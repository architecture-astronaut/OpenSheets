using System.Collections.Generic;

namespace OpenSheets.Contracts.Commands
{
    public class ValidatePatchResponse
    {
        public IEnumerable<ValidationResult> Results { get; set; }
    }
}