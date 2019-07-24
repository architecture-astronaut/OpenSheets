using System;
using System.Collections.Generic;

namespace OpenSheets.Core
{
    public class Field
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public HashSet<FieldFlag> Flags { get; set; }
        public FieldKind Kind { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}