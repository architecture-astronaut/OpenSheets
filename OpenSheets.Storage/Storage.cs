using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace OpenSheets.Storage
{
    public class Storage<T>
    {
        public IMongoCollection<T> Collection { get; }
        public IMongoCollection<T> Historical { get; }

        internal Storage(IClientSession session, string database, string collection, string historical = null)
        {
            Collection = session.Client.GetDatabase(database).GetCollection<T>(collection);

            Historical = session.Client.GetDatabase(database).GetCollection<T>(historical ?? $"{collection}_historical");
        }
    }
}
