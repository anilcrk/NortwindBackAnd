using Core.Utilities.Interceptors.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extentios
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection services,ICoreModule[] coreModules)
        {
            foreach (var module in coreModules)
            {
                module.Load(services);  //Bütün modullerimi bu operasyon vasıtasıyla .net corea yüklemiş olacağım.
            }

            return ServiceTool.Create(services);
        }
    }
}
