using System;
using System.Web.Configuration;
using MassTransit;

namespace OpenSheets.Api
{
    public static class BusConfig
    {
        public static void Register(SimpleInjector.Container container)
        {
            string endpointAddress = WebConfigurationManager.AppSettings["bus.transport.endpoint"];
            string endpointUsername = WebConfigurationManager.AppSettings["bus.transport.endpoint.username"];
            string endpointPassword = WebConfigurationManager.AppSettings["bus.transport.endpoint.password"];

            container.AddMassTransit(mt =>
                //mt.AddConsumers();

                mt.AddBus(() =>
                    Bus.Factory.CreateUsingRabbitMq(cfg =>
                        {
                            cfg.Host(new Uri(endpointAddress),
                                host =>
                                {
                                    host.Username(endpointUsername);
                                    host.Password(endpointPassword);
                                });
                        }
                    )));
        }
    }
}