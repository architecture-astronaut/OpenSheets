using System;
using Marvin.JsonPatch;

namespace OpenSheets.Contracts.Commands
{
    public class ValidatePatchRequest<T> where T : class
    {
        public Guid ObjectId { get; set; }
        //Role-checking
        public JsonPatchDocument<T> ProposedPatch { get; set; }
    }
}