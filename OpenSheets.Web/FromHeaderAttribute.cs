using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace OpenSheets.Web
{
    [AttributeUsage(AttributeTargets.Parameter)]
    [ExcludeFromCodeCoverage]
    public sealed class FromHeaderAttribute : ModelBinderAttribute
    {
        /// <inheritdoc />
        public override IEnumerable<ValueProviderFactory> GetValueProviderFactories(HttpConfiguration configuration)
        {
            return new[] { new FromHeaderValueProviderFactory() };
        }

        private class FromHeaderValueProviderFactory : ValueProviderFactory
        {
            public override IValueProvider GetValueProvider(HttpActionContext actionContext)
            {
                return new FromHeaderProvider(actionContext, CultureInfo.CurrentCulture);
            }
        }
    }
}