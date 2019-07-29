using System;

namespace OpenSheets.Contracts.Commands
{
    public class UpdateCommand<T> where T : class
    {
        public Guid ObjectId { get; set; }
        public Guid NewVersion { get; set; }
        public T Object { get; set; }
    }

    public class RemoveCommand<T> where T : class
    {
        public Guid ObjectId { get; set; }
        public T Object { get; set; }
    }

    public class CreateCommand<T> where T : class
    {
        public T Object { get; set; }
    }
}