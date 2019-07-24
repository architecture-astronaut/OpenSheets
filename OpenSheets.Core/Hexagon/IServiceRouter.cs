using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotImplementedException = System.NotImplementedException;

namespace OpenSheets.Core.Hexagon
{
    public interface IServiceRouter
    {
        TResult Query<TRequest, TResult>(TRequest request);
        Task<TResult> QueryAsync<TRequest, TResult>(TRequest request);
        void Command<TRequest>(TRequest request);
        Task CommandAsync<TRequest>(TRequest request);
    }

    public class PushEvent
    {
        public object Event { get; set; }
    }
}
