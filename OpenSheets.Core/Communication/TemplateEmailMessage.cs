using System.Collections.Generic;

namespace OpenSheets.Service.Comm
{
    public class TemplateEmailMessage : EmailMessage
    {
        public string TemplateId { get; set; }
        public Dictionary<string, object> TemplateData { get; set; }
    }
}