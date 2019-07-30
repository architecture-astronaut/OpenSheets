using System.Collections.Generic;

namespace OpenSheets.Contracts.Commands
{
    public class GetCollectionResponse<T>
    {
        public IEnumerable<T> Results { get; set; }
    }
}