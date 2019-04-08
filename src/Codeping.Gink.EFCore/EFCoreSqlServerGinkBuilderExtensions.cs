using Codeping.Gink.EFCore;
using Microsoft.Extensions.DependencyInjection;

namespace Codeping.Gink.Core
{
    public static class EFCoreSqlServerGinkBuilderExtensions
    {
        public static GinkBuilder AddSqlServer(this GinkBuilder options)
        {
            options.Services.AddScoped<IGinkSession, EFCoreSqlServerGinkSession>();

            return options;
        }
    }
}
