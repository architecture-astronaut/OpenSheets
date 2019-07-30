namespace OpenSheets.Contracts.Commands
{
    public class GetPagedCollectionResponse<T> : GetCollectionResponse<T>
    {
        public int Page { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }
    }
}