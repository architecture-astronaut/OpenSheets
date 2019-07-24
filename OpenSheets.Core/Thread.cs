using System;
using System.Collections.Generic;

namespace OpenSheets.Core
{
    public class Thread
    {
        public Guid Id { get; set; }
        public Guid SubjectId { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public ThreadKind Kind { get; set; }
        public HashSet<ThreadFlag> Flags { get; set; }
        public Dictionary<string, object> Metadata { get; set; }
        public DateTime Created { get; set; }
    }
}