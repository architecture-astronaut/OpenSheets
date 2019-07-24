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

        internal Storage(IClientSession session, string database, string collection)
        {
            Collection = session.Client.GetDatabase(database).GetCollection<T>(collection);
        }
    }
}
