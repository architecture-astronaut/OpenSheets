using System;

namespace OpenSheets.Contracts.Commands
{
    public class RemoveCommand<T> where T : class
    {
        public Guid ObjectId { get; set; }
        public T Object { get; set; }
    }
}