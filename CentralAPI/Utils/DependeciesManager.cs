using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CentralAPI.Utils
{
    public static class DependeciesManager
    {
        public static void AddAplicationRepo(this IServiceCollection service)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var services = assembly.GetTypes().Where(type => 
                           type.GetTypeInfo().IsClass &&
                           type.Name.EndsWith("Repository") &&
                           !type.GetTypeInfo().IsAbstract);

            foreach ( var serviceType in services)
            {
                var allInterfaces = serviceType.GetInterfaces();
                var mainInterfaces = allInterfaces.Except(allInterfaces.SelectMany(t => t.GetInterfaces()));

                foreach (var iSeviceType in mainInterfaces)
                {
                    service.AddTransient(iSeviceType, serviceType);
                }
            }
        }
        public static void AddAplicationService(this IServiceCollection service)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var services = assembly.GetTypes().Where(type =>
                           type.GetTypeInfo().IsClass &&
                           type.Name.EndsWith("Service") &&
                           !type.GetTypeInfo().IsAbstract);

            foreach (var serviceType in services)
            {
                var allInterfaces = serviceType.GetInterfaces();
                var mainInterfaces = allInterfaces.Except(allInterfaces.SelectMany(t => t.GetInterfaces()));

                foreach (var iSeviceType in mainInterfaces)
                {
                    service.AddTransient(iSeviceType, serviceType);
                }
            }
        }

    }
}
