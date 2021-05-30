using MarsRoverControl.Interfaces;
using Microsoft.Extensions.DependencyInjection;

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
