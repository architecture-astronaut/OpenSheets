using System;

namespace OpenSheets.File.Controllers
{
    public class CheckPermissionRequest
    {
        public Guid IdentityId { get; set; }
        public Guid OwnerId { get; set; }
        public Guid FileId { get; set; }
    }
}