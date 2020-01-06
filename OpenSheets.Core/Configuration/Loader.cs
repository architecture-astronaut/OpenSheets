using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace OpenSheets.Core.Configuration
{
    public static class Loader
    {
        public static object LoadConfig(Type type)
        {
            if (!type.CustomAttributes.Any(x => x.AttributeType == typeof(ConfigurationClassAttribute)))
            {
                throw new ArgumentException();
            }

            ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);

            if (constructor == null)
            {
                throw new Exception();
            }

            ConfigurationClassAttribute confClassAttr = type.GetCustomAttribute<ConfigurationClassAttribute>();

            if (confClassAttr == null)
            {
                throw new Exception();
            }

            object obj = Activator.CreateInstance(type);

            foreach (var prop in type.GetProperties())
            {
                ConfigurationItemAttribute attr = prop.GetCustomAttribute<ConfigurationItemAttribute>();

                if (attr == null)
                {
                    continue;
                }

                string tag = $"{confClassAttr.Prefix}::{attr.Name}";

                string confValue = ConfigurationManager.AppSettings[tag];

                object val = AttemptParse(confValue, prop.PropertyType);

                prop.SetValue(obj, val);
            }

            return obj;
        }

        private static object AttemptParse(string str, Type type)
        {
            MethodInfo method = typeof(int).GetMethod("Parse", BindingFlags.Public | BindingFlags.Static, null, CallingConventions.Any, new[] { typeof(string) }, null);

            object obj = method.Invoke(null, BindingFlags.Default, null, new[] { str }, CultureInfo.CurrentCulture);

            return obj;
        }

        public static T LoadConfig<T>() where T : class
        {
            return LoadConfig(typeof(T)) as T;
        }
    }
}