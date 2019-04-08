using Codeping.Gink.Core;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GinkServiceCollectionExtensions
    {
        public static IServiceCollection AddGoLink(this IServiceCollection services)
        {
            services.AddScoped<IGoLinkService, GinkService>();

            services.AddSingleton<IGinkSession, DefaultGinkSession>();

            services.AddScoped<GinkOptions>();

            return services;
        }

        public static IServiceCollection AddGink(this IServiceCollection services, Action<GinkOptions> setupAction)
        {
            services.AddScoped<IGoLinkService, GinkService>();

            services.AddSingleton<IGinkSession, DefaultGinkSession>();

            services.AddScoped(x =>
            {
                var options = new GinkOptions(services);

                setupAction?.Invoke(options);

                return options;
            });

            return services;
        }
    }
}
