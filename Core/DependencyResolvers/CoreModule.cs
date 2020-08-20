using Core.CrossCuttingConcerns.Cashing;
using Core.CrossCuttingConcerns.Cashing.Microsoft;
using Core.Utilities.Interceptors.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICasheManager, MemoryCasheManager>();//birisi senden ICasheManager isterse MemoryCasheManager ver.
        }
    }
}
