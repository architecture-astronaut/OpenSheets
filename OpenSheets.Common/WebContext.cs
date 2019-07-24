using System;
using System.Collections.Generic;
using Microsoft.Owin.Infrastructure;
using UAParser;

namespace OpenSheets.Common
{
    public class WebContext
    {
        public ISystemClock Clock { get; set; }
        public ServerConfiguration ServerConfig { get; set; }
        public ClientInfo Client { get; set; }
    }

    public class ServerConfiguration
    {
        public AuthenticationConfiguration AuthConfig { get; set; }
    }

    public class AuthenticationConfiguration
    {
        public byte[] TokenKey { get; set; }
        public string TokenAlgorithm { get; set; }
        public IEnumerable<TokenSpec> TokenSpecs { get; set; }
        public bool IndicateBadResetEmail { get; set; }
    }

    public class TokenSpec
    {
        public TokenType Type { get; set; }
        public TimeSpan Duration { get; set; }
    }
}