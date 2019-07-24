using System;
using System.Collections.Generic;

namespace OpenSheets.Core
{
    public class Post
    {
        public Guid Id { get; set; }
        public Guid ThreadId { get; set; }
        public string Content { get; set; }
        public HashSet<GroupFlag> Flags { get; set; }
        public Dictionary<string, object> Metadata { get; set; }
        public GroupKind Kind { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}