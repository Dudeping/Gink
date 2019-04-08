using Codeping.Gink.EFCore;
using Microsoft.Extensions.DependencyInjection;

namespace Codeping.Gink.Core
{
    public static class EFCoreSqlServerGinkOptionsExtensions
    {
        public static GinkOptions UseSqlServer(this GinkOptions options)
        {
            options.Services.AddScoped<IGinkSession, EFCoreSqlServerGinkSession>();

            return options;
        }
    }
}
