using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace OpenSheets.Core
{
    public class File
    {
        public Guid Id { get; set; }
        public Guid DirectoryId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Dictionary<string, object> Metadata { get; set; }
        public HashSet<FileFlag> Flags { get; set; }
        public int Length { get; set; }
        public JObject Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Guid Version { get; set; }
    }

    public class FilePermission
    {
        public Guid Id { get; set; }
        public Guid SubjectId { get; set; }
        public Dictionary<FilePermissionAction, bool> Action { get; set; }
        public FilePermissionKind Kind { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }

    public enum FilePermissionAction
    {
        List, //Appear in enumeration
        Read, //View contents
        Write,
        Delete
    }

    public enum FilePermissionKind
    {
        Identity,
        Role
    }
}