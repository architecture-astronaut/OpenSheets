using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lambda2Js;

namespace OpenSheets.Core.Utilities
{
    public static class PathExtensions
    {
        public static string ToPath<T>(this Expression<Func<T, object>> expr)
        {
            string[] exprParts = new[] {"$", expr.CompileToJavascript()};

            return string.Join(".", exprParts.Where(x => !string.IsNullOrWhiteSpace(x)));
        }
    }
}