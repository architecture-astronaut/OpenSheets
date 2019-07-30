using System;

namespace OpenSheets.File.Controllers
{
    public class BuildPathRequest
    {
        public Guid SubjectId { get; set; }
        public Guid FileId { get; set; }
    }
}