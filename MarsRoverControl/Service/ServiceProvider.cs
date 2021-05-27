using MarsRoverControl.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverControl.Service
{
    public class InjectionServiceProvider
    {
        public static ServiceProvider Builder()
        {
            IServiceCollection services = new ServiceCollection();
           
            services.AddSingleton<ISurface, Surface>();

            return services.BuildServiceProvider();
        }
    }
}
