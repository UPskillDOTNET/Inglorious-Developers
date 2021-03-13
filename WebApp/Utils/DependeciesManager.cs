using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WebApp.Utils
{
    public static class DependeciesManager
    {
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
