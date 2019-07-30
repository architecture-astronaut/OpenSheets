using System.Collections.Generic;
using OpenSheets.Core;

namespace OpenSheets.File.Controllers
{
    public class CheckPermissionResponse
    {
        public Dictionary<FilePermissionAction, bool> EffectivePermissions { get; set; }
    }
}