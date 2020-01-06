using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenSheets.Core.Configuration
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
    public class ConfigurationClassAttribute : Attribute
    {
        public string Prefix { get; }

        public ConfigurationClassAttribute(string prefix)
        {
            Prefix = prefix;
        }
    }
}
