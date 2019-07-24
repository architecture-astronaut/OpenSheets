using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace OpenSheets.Core
{
    public class File
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public FileType Type { get; set; }
        public Dictionary<string, object> Metadata { get; set; }
        public HashSet<FileFlag> Flags { get; set; }
        public int Length { get; set; }
        public JObject Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Version { get; set; }
    }
}