using System.Threading.Tasks;

namespace OpenSheets.Core.Hexagon
{
    public abstract class HandleQuery<TRequest, TResult>
    {
        public virtual async Task<TResult> QueryAsync(TRequest request, IServiceRouter router, RequestContext context)
        {
            return await new Task<TResult>(() =>
            {
                using (ContextScope scope = new ContextScope(context))
                {
                    return Query(request, router, scope.Current);
                }
            }).ConfigureAwait(false);
        }

        public abstract TResult Query(TRequest request, IServiceRouter router, RequestContext context);
    }
}