using System;

namespace OpenSheets.Core.Configuration
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ConfigurationItemAttribute : Attribute
    {
        public ConfigurationItemAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}