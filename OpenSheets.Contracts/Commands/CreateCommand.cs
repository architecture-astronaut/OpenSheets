namespace OpenSheets.Contracts.Commands
{
    public class CreateCommand<T> where T : class
    {
        public T Object { get; set; }
    }
}