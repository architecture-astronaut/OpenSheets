using Microsoft.Owin.Infrastructure;
using OpenSheets.Core;
using UAParser;

namespace OpenSheets.Web
{
    public interface IWebContext
    {
        ISystemClock Clock { get; }
        ServerConfiguration ServerConfig { get; }
        ClientInfo Client { get; }
        Principal Principal { get; }
        Identity Identity { get; }
    }
}