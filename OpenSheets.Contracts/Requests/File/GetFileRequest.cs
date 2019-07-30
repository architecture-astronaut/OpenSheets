using System;

namespace OpenSheets.File.Controllers
{
    public class GetFileByIdRequest
    {
        public Guid OwnerId { get; set; }
        public Guid FileId { get; set; }
    }
}