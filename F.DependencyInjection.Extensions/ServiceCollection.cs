using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace F.DependencyInjection.Extensions
{
    public static class ServiceCollection
    {
        public static void AddTransientFromAssembly<TService>(
            this IServiceCollection services)
             where TService : class
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (typeof(TService) == null)
            {
                throw new ArgumentNullException(nameof(TService));
            }
            AddServices(services, typeof(TService), ServiceLifetime.Transient);
        }
        public static void AddTransientScan<TService>(
            this IServiceCollection services)
             where TService : class
        {
            services.AddTransientFromAssembly<TService>();
        }

        public static void AddTransientFromAssembly(
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
            Type serviceType)
        {
            services.AddTransientFromAssembly(serviceType);
        }
        public static void AddTransientFromAssembly(
            this IServiceCollection services,
            string assembly, Action<DIOptions> options = default)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (assembly == null)
            {
                throw new ArgumentNullException(assembly);
            }
            var opts = new DIOptions();
            options.Invoke(opts);
            AddServices(services, assembly, opts.Matching, ServiceLifetime.Transient);
        }
        public static void AddTransientScan(
            this IServiceCollection services,
            string assembly, Action<DIOptions> options = default)
        {
            services.AddTransientFromAssembly(assembly, options);
        }

        public static void AddScopedFromAssembly<TService>(
            this IServiceCollection services)
            where TService : class
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (typeof(TService) == null)
            {
                throw new ArgumentNullException(nameof(TService));
            }
            AddServices(services, typeof(TService), ServiceLifetime.Scoped);
        }
        public static void AddScopedScan<TService>(
            this IServiceCollection services)
            where TService : class
        {
            services.AddScopedFromAssembly<TService>();
        }
        public static void AddScopedFromAssembly(
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
            Type serviceType)
        {
            services.AddScopedFromAssembly(serviceType);
        }
        public static void AddScopedFromAssembly(
            this IServiceCollection services,
            string assembly, Action<DIOptions> options = default)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (assembly == null)
            {
                throw new ArgumentNullException(assembly);
            }
            var opts = new DIOptions();
            options?.Invoke(opts);
            AddServices(services, assembly, opts.Matching, ServiceLifetime.Scoped);
        }
        public static void AddScopedScan(
            this IServiceCollection services,
            string assembly, Action<DIOptions> options = default)
        {
            services.AddScopedFromAssembly(assembly, options);
        }


        public static void AddSingletonFromAssembly<TService>(
            this IServiceCollection services)
            where TService : class
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (typeof(TService) == null)
            {
                throw new ArgumentNullException(nameof(TService));
            }
            AddServices(services, typeof(TService), ServiceLifetime.Singleton);
        }
        public static void AddSingletonScan<TService>(
            this IServiceCollection services)
            where TService : class
        {
            services.AddSingletonFromAssembly<TService>();
        }
        public static void AddSingletonFromAssembly(
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
            Type serviceType)
        {
            services.AddSingletonFromAssembly(serviceType);
        }
        public static void AddSingletonFromAssembly(
            this IServiceCollection services,
            string assembly, Action<DIOptions> options = default)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (assembly == null)
            {
                throw new ArgumentNullException(assembly);
            }
            var opts = new DIOptions();
            options?.Invoke(opts);
            AddServices(services, assembly, opts.Matching, ServiceLifetime.Singleton);
        }
        public static void AddSingletonScan(
            this IServiceCollection services,
            string assembly, Action<DIOptions> options = default)
        {
            services.AddSingletonFromAssembly(assembly, options);
        }


        private static void AddServices(
            IServiceCollection services,
            Type type,
            ServiceLifetime lifetime)
        {
            var types = ServiceProvider.AssemblyScan(type.Assembly);
            AddServices(services, types, lifetime);
        }
        private static void AddServices(
            IServiceCollection services,
            string assembly,
            bool matching,
            ServiceLifetime lifetime)
        {
            if (matching)
            {
                var assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, $"*{assembly}*.dll");
                foreach (var item in assemblies)
                {
                    var assemblyName = Path.GetFileNameWithoutExtension(item);
                    var itemTypes = ServiceProvider.AssemblyScan(Assembly.Load(assemblyName));
                    AddServices(services, itemTypes, lifetime);
                }
                return;
            }
            var types = ServiceProvider.AssemblyScan(Assembly.Load(assembly));
            AddServices(services, types, lifetime);
        }
        private static void AddServices(
            IServiceCollection services,
            Type[] types,
            ServiceLifetime lifetime)
        {
            foreach (var type in types)
            {
                Add(services, type, type, lifetime);
                var interfaces = type.GetInterfaces();
                foreach (var _interface in interfaces)
                {
                    Add(services, _interface, type, lifetime);
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
