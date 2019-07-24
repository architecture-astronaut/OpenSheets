using System;
using System.Collections.Generic;

namespace OpenSheets.Core
{
    public class License
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<string, string> Terms { get; set; }
        public Dictionary<string, object> Metadata { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}