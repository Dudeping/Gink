using Codeping.GoLink.Core;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GoLinkServiceCollectionExtensions
    {
        public static IServiceCollection AddGoLink(this IServiceCollection services)
        {
            services.AddScoped<IGoLinkService, GoLinkService>();

            services.AddSingleton<IGoLinkSession, DefaultGoLinkSession>();

            services.AddScoped<GoLinkOptions>();

            return services;
        }

        public static IServiceCollection AddGoLink(this IServiceCollection services, Action<GoLinkOptions> setupAction)
        {
            services.AddScoped<IGoLinkService, GoLinkService>();

            services.AddSingleton<IGoLinkSession, DefaultGoLinkSession>();

            services.AddScoped(x =>
            {
                var options = new GoLinkOptions(services);

                setupAction?.Invoke(options);

                return options;
            });

            return services;
        }
    }
}
