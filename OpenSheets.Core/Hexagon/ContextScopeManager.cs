using System;
using System.Collections.Generic;

namespace OpenSheets.Core.Hexagon
{
    internal static class ContextScopeManager
    {
        [ThreadStatic]
        private static Stack<RequestContext> _contextStack;

        public static RequestContext Current => _contextStack.Peek();

        public static void Back()
        {
            _contextStack.Pop();
        }

        public static void Next(RequestContext context)
        {
            if (_contextStack == null)
            {
                _contextStack = new Stack<RequestContext>();
            }

            _contextStack.Push(context);

        }
    }
}