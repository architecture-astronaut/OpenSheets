using System.Collections.Generic;

namespace OpenSheets.File.Controllers
{
    public class EnumerateDirectoryResponse
    {
        public Core.File Directory { get; set; }
        public IEnumerable<Core.File> Contents { get; set; }
    }
}