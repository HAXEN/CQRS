using Microsoft.Extensions.DependencyInjection;
using System;

namespace CQRS.Commanding
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddCommanding(this IServiceCollection services, Action<Configuration> config = null)
        {
            var configuration = new Configuration();
            config?.Invoke(configuration);

            services.AddTransient<ICommandMediator>(provider => ICommandMediator.Create(configuration));

            return services;
        }
    }
}
