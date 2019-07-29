using System;
using Marvin.JsonPatch;

namespace OpenSheets.Contracts.Commands
{
    public class PatchCommand<T> where T : class
    {
        public Guid ObjectId { get; set; }
        public Guid NewVersion { get; set; }
        public JsonPatchDocument<T> Patch { get; set; }
    }
}