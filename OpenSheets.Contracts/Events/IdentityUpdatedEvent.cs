using System;

namespace OpenSheets.Services.Handlers
{
    public class IdentityUpdatedEvent
    {
        public Guid IdentityId { get; set; }
        public Guid NewVersion { get; set; }
        public Guid OldVersion { get; set; }
    }
}