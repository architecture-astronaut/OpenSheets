using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Marvin.JsonPatch;
using OpenSheets.Contracts.Commands;
using OpenSheets.Core;
using OpenSheets.Core.Hexagon;
using OpenSheets.Core.Utilities;
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
        [Route("api/files/{subjectId}/{directoryId}")]
        public HttpResponseMessage GetFiles(Guid subjectId, Guid directoryId, string filter = "*", bool showHidden = false, bool showSystem = false)
        {
            HashSet<FileFlag> flags = new HashSet<FileFlag>();

            if (!Context.Identity.Flags.Contains(IdentityFlag.SysAdmin) || !showSystem)
            {
                flags.Add(FileFlag.System);
            }

            CheckPermissionResponse permissionResponse = _router.Query<CheckPermissionRequest, CheckPermissionResponse>(new CheckPermissionRequest()
            {
                IdentityId = Context.Identity.Id,
                OwnerId = subjectId,
                FileId = directoryId
            });

            bool canSee = false;

            if (!permissionResponse.EffectivePermissions.TryGetValue(FilePermissionAction.Read, out canSee) || !canSee)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            if (!showHidden)
            {
                flags.Add(FileFlag.Hidden);
            }

            EnumerateDirectoryResponse enumerateResponse = _router.Query<EnumerateDirectoryRequest, EnumerateDirectoryResponse>(
                new EnumerateDirectoryRequest()
                {
                    RequesterId = Context.Identity.Id,
                    SubjectId = subjectId,
                    DirectoryId = directoryId,
                    ExcludeFlags = flags,
                    IncludeFlags = new HashSet<FileFlag>(),
                    Filter = filter
                });

            BuildPathResponse pathResponse = _router.Query<BuildPathRequest, BuildPathResponse>(new BuildPathRequest()
            {
                SubjectId = subjectId,
                FileId = directoryId
            });

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                Path = pathResponse.Path,
                Contents = enumerateResponse.Contents.Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Type,
                    x.Flags,
                    x.Length,
                    x.Created,
                    x.Updated
                })
            });
        }

        [HttpGet]
        [Route("api/file/{userId}/{fileId}")]
        public HttpResponseMessage GetFileDetails(Guid userId, Guid fileId)
        {
            CheckPermissionResponse permissionResponse = _router.Query<CheckPermissionRequest, CheckPermissionResponse>(new CheckPermissionRequest()
            {
                IdentityId = Context.Identity.Id,
                OwnerId = userId,
                FileId = fileId
            });

            bool canSee = false;

            if (!permissionResponse.EffectivePermissions.TryGetValue(FilePermissionAction.Read, out canSee) || !canSee)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            GetFileResponse fileResp = _router.Query<GetFileRequest, GetFileResponse>(new GetFileRequest()
            {
                OwnerId = userId,
                FileId = fileId
            });

            return Request.CreateResponse(HttpStatusCode.OK, fileResp.File);
        }

        [HttpPost]
        [Route("api/file/{userId}/{directoryId}")]
        public HttpResponseMessage CreateFile(Guid userId, Guid directoryId, Core.File fileData)
        {
            if (fileData.DirectoryId == Guid.Empty)
            {
                fileData.DirectoryId = directoryId;
            }

            CheckPermissionResponse permissionResponse = _router.Query<CheckPermissionRequest, CheckPermissionResponse>(new CheckPermissionRequest()
            {
                IdentityId = Context.Identity.Id,
                OwnerId = userId,
                FileId = directoryId
            });

            bool canWrite = false;

            if (!permissionResponse.EffectivePermissions.TryGetValue(FilePermissionAction.Write, out canWrite) || !canWrite)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            fileData.Id = Guid.NewGuid();

            ValidateResponse validateResp = _router.Query<ValidateRequest<Core.File>, ValidateResponse>(new ValidateRequest<Core.File>()
            {
                ObjectId = fileData.Id,
                Object = fileData
            });

            if (validateResp.Results.Any(x => x.Level > Level.Information))
            {
                return Request.CreateResponse((HttpStatusCode)422, new { Validation = new { Errors = validateResp.Results } });
            }

            _router.Command(new CreateCommand<Core.File>()
            {
                Object = fileData
            });

            return Request.CreateResponse(HttpStatusCode.OK, new {FileId = fileData.Id});
        }

        [HttpPut]
        [Route("api/file/{userId}/{fileId}")]
        public HttpResponseMessage UpdateFile(Guid userId, Guid fileId, Guid version, [FromBody]Core.File fileData, [FromHeader(Name = "opensheets-bypass-level")] Level bypassLevel = Level.Information)
        {
            if (bypassLevel > Level.Warning && (Level)Context.Principal.Metadata["Allowed-Bypass"] < bypassLevel)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, new { Reason = $"Attempted to bypass validation of {bypassLevel} level, only allowed { (Level?)Context.Principal.Metadata["Allowed-Bypass"] ?? Level.Warning }" });
            }

            GetFileResponse response = _router.Query<GetFileByIdRequest, GetFileResponse>(new GetFileByIdRequest()
            {
                FileId = fileId
            });

            if (response.File == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (response.File.Version != version)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict);
            }

            CheckPermissionResponse permissionResponse = _router.Query<CheckPermissionRequest, CheckPermissionResponse>(new CheckPermissionRequest()
            {
                IdentityId = Context.Identity.Id,
                OwnerId = userId,
                FileId = fileId
            });

            bool canWrite = false;

            if (!permissionResponse.EffectivePermissions.TryGetValue(FilePermissionAction.Write, out canWrite) || !canWrite)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            fileData.Id = Guid.NewGuid();

            ValidateResponse validateResp = _router.Query<ValidateRequest<Core.File>, ValidateResponse>(new ValidateRequest<Core.File>()
            {
                ObjectId = fileData.Id,
                Object = fileData
            });

            if (validateResp.Results.Any(x => x.Level > Level.Information))
            {
                return Request.CreateResponse((HttpStatusCode)422, new { Validation = new { Errors = validateResp.Results } });
            }

            Guid newVer = Guid.NewGuid();

            _router.Command(new UpdateCommand<Core.File>()
            {
                ObjectId = fileId,
                Object = fileData,
                NewVersion = newVer
            });

            return Request.CreateResponse(HttpStatusCode.OK, new { Version = newVer });
        }

        [HttpPatch]
        [Route("api/file/{userId}/{fileId}")]
        public HttpResponseMessage PatchFile(Guid userId, Guid fileId, Guid version, [FromBody]JsonPatchDocument<Core.File> model, [FromHeader(Name = "opensheets-bypass-level")] Level bypassLevel = Level.Information)
        {
            if (bypassLevel > Level.Warning && (Level)Context.Principal.Metadata["Allowed-Bypass"] < bypassLevel)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, new { Reason = $"Attempted to bypass validation of {bypassLevel} level, only allowed { (Level?)Context.Principal.Metadata["Allowed-Bypass"] ?? Level.Warning }" });
            }

            GetFileResponse response = _router.Query<GetFileByIdRequest, GetFileResponse>(new GetFileByIdRequest()
            {
                FileId = fileId
            });

            if (response.File == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            CheckPermissionResponse permissionResponse = _router.Query<CheckPermissionRequest, CheckPermissionResponse>(new CheckPermissionRequest()
            {
                IdentityId = Context.Identity.Id,
                OwnerId = userId,
                FileId = fileId
            });

            bool canWrite = false;

            if (!permissionResponse.EffectivePermissions.TryGetValue(FilePermissionAction.Write, out canWrite) || !canWrite)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            if (response.File.Version != version)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict);
            }

            ValidatePatchResponse validateResp = _router.Query<ValidatePatchRequest<Core.File>, ValidatePatchResponse>(new ValidatePatchRequest<Core.File>()
            {
                ObjectId = fileId,
                ProposedPatch = model
            });

            if (validateResp.Results.Any(x => x.Level > bypassLevel))
            {
                return Request.CreateResponse((HttpStatusCode)422, new { Validation = new { Errors = validateResp.Results } });
            }

            PatchCommand<Core.File> request = new PatchCommand<Core.File>()
            {
                NewVersion = Guid.NewGuid(),
                Patch = model
            };

            _router.Command(request);

            return Request.CreateResponse(HttpStatusCode.OK, new { Version = request.NewVersion });
        }

        [HttpDelete]
        [Route("api/file/{userId}/{fileId}")]
        public HttpResponseMessage DeleteFile(Guid userId, Guid fileId)
        {
            GetFileResponse response = _router.Query<GetFileByIdRequest, GetFileResponse>(new GetFileByIdRequest()
            {
                FileId = fileId
            });

            if (response.File == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            
            CheckPermissionResponse permissionResponse = _router.Query<CheckPermissionRequest, CheckPermissionResponse>(new CheckPermissionRequest()
            {
                IdentityId = Context.Identity.Id,
                OwnerId = userId,
                FileId = fileId
            });

            bool canDelete = false;

            if (!permissionResponse.EffectivePermissions.TryGetValue(FilePermissionAction.Write, out canDelete) || !canDelete)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            _router.Command(new RemoveCommand<Core.File>()
            {
                ObjectId = fileId,
                Object = response.File
            });

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }

    public class GetFileByIdRequest
    {
        public Guid FileId { get; set; }
    }

    public class GetFileResponse
    {
        public Core.File File { get; set; }
    }

    public class GetFileRequest
    {
        public Guid OwnerId { get; set; }
        public Guid FileId { get; set; }
    }

    public class CheckPermissionRequest
    {
        public Guid IdentityId { get; set; }
        public Guid OwnerId { get; set; }
        public Guid FileId { get; set; }
    }

    public class CheckPermissionResponse
    {
        public Dictionary<FilePermissionAction, bool> EffectivePermissions { get; set; }
    }

    public class BuildPathRequest
    {
        public Guid SubjectId { get; set; }
        public Guid FileId { get; set; }
    }

    public class BuildPathResponse
    {
        public string Path { get; set; }
    }

    public class EnumerateDirectoryRequest
    {
        public Guid SubjectId { get; set; }
        public Guid DirectoryId { get; set; }
        public Guid? RequesterId { get; set; }
        public HashSet<FileFlag> IncludeFlags { get; set; }
        public HashSet<FileFlag> ExcludeFlags { get; set; }
        public string Filter { get; set; }
    }

    public class EnumerateDirectoryResponse
    {
        public Core.File Directory { get; set; }
        public IEnumerable<Core.File> Contents { get; set; }
    }
}
