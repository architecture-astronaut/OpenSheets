using System.Threading.Tasks;
using NotImplementedException = System.NotImplementedException;

namespace OpenSheets.Core.Hexagon
{
    public abstract class HandleCommand<TRequest>
    {
        public virtual async Task CommandAsync(TRequest request, IServiceRouter router, RequestContext context)
        {
            await new Task(() =>
            {
                using (ContextScope scope = new ContextScope(context))
                {
                    Command(request, router, scope.Current);
                }
            }).ConfigureAwait(false);
        }

        public abstract void Command(TRequest request, IServiceRouter router, RequestContext context);
    }
}