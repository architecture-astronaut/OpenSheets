using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using OpenSheets.Core.Dependency;
using OpenSheets.Core.Hexagon;
using SimpleInjector;

namespace OpenSheets.Api
{
    public static class ContainerConfig
    {
        public static void Build(SimpleInjector.Container container)
        {
            Discover(container);

        }

        private static void Discover(SimpleInjector.Container container)
        {
            Type assignable = typeof(IRegisterDependency);

            IEnumerable<Type> registerableTypes = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.FullName.StartsWith("OpenSheets.")).SelectMany(x => x.GetTypes())
                .Where(x => x.IsClass && assignable.IsAssignableFrom(x)).ToList();

            foreach (Type registerableType in registerableTypes)
            {
                IRegisterDependency registrar = Activator.CreateInstance(registerableType) as IRegisterDependency;

                registrar?.Register((service, iface) =>
                {
                    container.Register(service, iface, Lifestyle.Scoped);
                });
            }
        }
    }
}