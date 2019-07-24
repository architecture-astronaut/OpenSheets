using System;

namespace OpenSheets.Core.Hexagon
{
    public class ContextScope : IDisposable
    {
        private readonly RequestContext _requestContext;

        public ContextScope(RequestContext requestContext)
        {
            _requestContext = requestContext;
            ContextScopeManager.Next(requestContext);
        }

        public void Dispose()
        {
            ContextScopeManager.Back();
        }

        public static RequestContext Ambient => ContextScopeManager.Current;

        public RequestContext Current => _requestContext;
    }
}