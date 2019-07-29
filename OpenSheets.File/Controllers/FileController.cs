using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using OpenSheets.Core.Hexagon;
using OpenSheets.Web;

namespace OpenSheets.File.Controllers
{
    public class FileController : ContextedController
    {
        private readonly IServiceRouter _router;

        public FileController(IWebContext context, IServiceRouter router) : base(context)
        {
            _router = router;
        }

        [HttpGet]
        [Route("api/files/{userId}/{directoryId}")]
        public HttpResponseMessage GetFiles(Guid userId, Guid directoryId)
        {

        }

        [HttpGet]
        [Route("api/file/{userId}/{fileId}")]
        public HttpResponseMessage GetFileDetails(Guid userId, Guid fileId)
        {

        }

        [HttpPost]
        [Route("api/file/{userId}/{directoryId}")]
        public HttpResponseMessage CreateFile(Guid userId, Guid directoryId)
        {

        }

        [HttpPut]
        [Route("api/file/{userId}/{fileId}")]
        public HttpResponseMessage UpdateFile(Guid userId, Guid fileId)
        {

        }

        [HttpPatch]
        [Route("api/file/{userId}/{fileId}")]
        public HttpResponseMessage PatchFile(Guid userId, Guid fileId)
        {

        }

        [HttpDelete]
        [Route("api/file/{userId}/{fileId}")]
        public HttpResponseMessage DeleteFile(Guid userId, Guid fileId)
        {

        }
    }
}
