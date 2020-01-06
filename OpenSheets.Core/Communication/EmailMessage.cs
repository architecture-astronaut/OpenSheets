using System;
using System.Collections.Generic;

namespace OpenSheets.Service.Comm
{
    public abstract class EmailMessage
    {
        public Guid Id { get; set; }
        public string[] To { get; set; }
        public string[] Cc { get; set; }
        public string[] Bcc { get; set; }
        public string From { get; set; }
        public List<(string, string, byte[])> Attachments { get; set; }
    }
}