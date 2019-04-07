using Codeping.GoLink.EFCore;
using Microsoft.Extensions.DependencyInjection;

namespace Codeping.GoLink.Core
{
    public static class EFCoreSqlServerGoLinkOptionsExtensions
    {
        public static GoLinkOptions UseSqlServer(this GoLinkOptions options)
        {
            options.Services.AddScoped<IGoLinkSession, EFCoreSqlServerGoLinkSession>();

            return options;
        }
    }
}
