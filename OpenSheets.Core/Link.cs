using System;

namespace OpenSheets.Core
{
    public class Link
    {
        public Guid Id { get; set; }
        public Guid TargetId { get; set; }
        public LinkKind Kind { get; set; }
    }
}