using Codeping.Gink.Core;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GinkServiceCollectionExtensions
    {
        public static GinkBuilder AddGink(this IServiceCollection services)
        {
            services.AddScoped<IGinkService, GinkService>();

            services.AddSingleton<IGinkSession, DefaultGinkSession>();

            return new GinkBuilder(services);
        }
    }
}
