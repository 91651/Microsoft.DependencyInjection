using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace F.DependencyInjection.Extensions
{
    public static class BuilderExtensions
    {
        public static void AddScopedOfAssembly<T>(this IServiceCollection services)
            where T : class
        {
            var types = typeof(T).Assembly.GetTypes();
            var injectionClass = GetClass(types);
            foreach (var c in injectionClass)
            {
                var f = c.GetInterface($"I{c.Name}");
                if (f == null)
                {
                    services.AddScoped(c);
                }
                else
                {
                    services.AddScoped(f, c);
                }

            }
        }
        public static void AddScopedOfAssembly(this IServiceCollection services, string assembly)
        {
            var types = Assembly.Load(assembly).GetTypes();
            var injectionClass = GetClass(types);
            foreach (var c in injectionClass)
            {
                var f = c.GetInterface($"I{c.Name}");
                if (f == null)
                {
                    services.AddScoped(c);
                }
                else
                {
                    services.AddScoped(f, c);
                }

            }
        }

        private static Type[] GetClass(Type[] types)
        {
            var injectionClass = types.Where(c => c.IsClass && c.IsPublic && !c.IsSealed && !c.IsGenericType).ToArray();
            return injectionClass;
        }
    }
}
