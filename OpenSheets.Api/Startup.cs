using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Owin;
using OpenSheets.Api;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

[assembly:OwinStartup(typeof(OpenSheets.Api.Startup))]
namespace OpenSheets.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Container container = new Container();

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            ContainerConfig.Build(container);

            HttpConfiguration config = new HttpConfiguration();

            container.EnableHttpRequestMessageTracking(config);

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            container.RegisterWebApiControllers(config, AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.StartsWith("OpenSheets.")));

            BusConfig.Register(container);

            app.Use(async (context, next) => {
                using (AsyncScopedLifestyle.BeginScope(container))
                {
                    await next();
                }
            });

            app.UseWebApi(config);
        }
    }
}