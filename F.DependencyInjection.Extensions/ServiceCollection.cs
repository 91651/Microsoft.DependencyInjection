using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace F.DependencyInjection.Extensions
{
    public static class ServiceCollection
    {
        public static void AddTransientScan(
            this IServiceCollection services,
            Type serviceType)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }
            AddServices(services, serviceType, ServiceLifetime.Transient);
        }
        public static void AddTransientScan(
            this IServiceCollection services,
            string assembly)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (assembly == null)
            {
                throw new ArgumentNullException(assembly);
            }
            AddServices(services, assembly, ServiceLifetime.Transient);
        }
        public static void AddScopedScan(
            this IServiceCollection services,
            Type serviceType)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }
            AddServices(services, serviceType, ServiceLifetime.Scoped);
        }
        public static void AddScopedScan(
            this IServiceCollection services,
            string assembly)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (assembly == null)
            {
                throw new ArgumentNullException(assembly);
            }
            AddServices(services, assembly, ServiceLifetime.Scoped);
        }
        public static void AddSingletonScan(
            this IServiceCollection services,
            Type serviceType)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }
            AddServices(services, serviceType, ServiceLifetime.Singleton);
        }
        public static void AddSingletonScan(
            this IServiceCollection services,
            string assembly)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (assembly == null)
            {
                throw new ArgumentNullException(assembly);
            }
            AddServices(services, assembly, ServiceLifetime.Singleton);
        }

        private static void AddServices(
            IServiceCollection services,
            Type type,
            ServiceLifetime lifetime)
        {
            var assembly = type.Assembly;
            var types = ServiceProvider.AssemblyScan(assembly);
            AddServices(services, types, lifetime);
        }
        private static void AddServices(
            IServiceCollection services,
            string assembly,
            ServiceLifetime lifetime)
        {
            var _assembly = Assembly.Load(assembly);
            var types = ServiceProvider.AssemblyScan(_assembly);
            AddServices(services, types, lifetime);
        }
        private static void AddServices(
            IServiceCollection services,
            Type[] types,
            ServiceLifetime lifetime)
        {
            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces();
                if (interfaces.Any())
                {
                    foreach (var _interface in interfaces)
                    {
                        Add(services, _interface, type, lifetime);
                    }
                }
                else
                {
                    Add(services, type, type, lifetime);
                }
            }
        }

        private static IServiceCollection Add(
            IServiceCollection collection,
            Type serviceType,
            Type implementationType,
            ServiceLifetime lifetime)
        {
            var descriptor = new ServiceDescriptor(serviceType, implementationType, lifetime);
            collection.Add(descriptor);
            return collection;
        }
    }
}
