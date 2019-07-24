using System.Threading.Tasks;

namespace OpenSheets.Core.Hexagon
{
    public class ServiceRouter : IServiceRouter
    {
        private readonly IServiceResolver _resolver;

        public ServiceRouter(IServiceResolver resolver)
        {
            _resolver = resolver;
        }

        public TResult Query<TRequest, TResult>(TRequest request)
        {
            HandleQuery<TRequest, TResult> handler = _resolver.Resolve<HandleQuery<TRequest, TResult>>();

            TResult result = handler.Query(request, this, ContextScopeManager.Current);

            return result;
        }

        public async Task<TResult> QueryAsync<TRequest, TResult>(TRequest request)
        {
            HandleQuery<TRequest, TResult> handler = _resolver.Resolve<HandleQuery<TRequest, TResult>>();

            TResult result = await handler.QueryAsync(request, this, ContextScopeManager.Current);

            return result;
        }

        public void Command<TRequest>(TRequest request)
        {
            HandleCommand<TRequest> handler = _resolver.Resolve<HandleCommand<TRequest>>();

            handler.Command(request, this, ContextScopeManager.Current);
        }

        public async Task CommandAsync<TRequest>(TRequest request)
        {
            HandleCommand<TRequest> handler = _resolver.Resolve<HandleCommand<TRequest>>();

            await handler.CommandAsync(request, this, ContextScopeManager.Current);
        }
    }
}