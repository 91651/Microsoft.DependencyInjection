using System;
using System.Linq;
using System.Reflection;

namespace F.DependencyInjection.Extensions
{
    static class ServiceProvider
    {
        public static Type[] AssemblyScan(Assembly assembly)
        {
            var types = assembly.GetTypes();
            var injectionTypes = types.Where(c => c.IsClass && c.IsPublic && !c.IsSealed && !c.IsGenericType).ToArray();
            return injectionTypes;
        }
    }
}
