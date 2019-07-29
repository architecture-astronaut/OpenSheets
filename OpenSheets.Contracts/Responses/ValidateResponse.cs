using System.Collections.Generic;

namespace OpenSheets.Contracts.Commands
{
    public class ValidateResponse
    {
        public IEnumerable<ValidationResult> Results { get; set; }
    }
}