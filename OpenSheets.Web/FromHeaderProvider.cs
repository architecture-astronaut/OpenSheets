using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders.Providers;

namespace OpenSheets.Web
{
    public class FromHeaderProvider : NameValuePairsValueProvider
    {
        public FromHeaderProvider(HttpActionContext actionContext, CultureInfo culture)
            : base(
                () => actionContext.ControllerContext.Request.Headers
                    .Where(x => x.Value.Count() == 1)
                    .Select(x => new KeyValuePair<string, string>(x.Key, x.Value.First())), culture)
        {
        }
    }
}