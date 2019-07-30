using System;
using System.Collections.Generic;
using OpenSheets.Core;

namespace OpenSheets.File.Controllers
{
    public class EnumerateDirectoryRequest
    {
        public Guid SubjectId { get; set; }
        public Guid DirectoryId { get; set; }
        public Guid? RequesterId { get; set; }
        public HashSet<FileFlag> IncludeFlags { get; set; }
        public HashSet<FileFlag> ExcludeFlags { get; set; }
        public string Filter { get; set; }
    }
}