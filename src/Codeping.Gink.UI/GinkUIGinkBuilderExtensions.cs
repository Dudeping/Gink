using Codeping.Gink.UI;
using Microsoft.Extensions.DependencyInjection;

namespace Codeping.Gink.Core
{
    public static class GinkUIGinkBuilderExtensions
    {
        public static GinkBuilder AddDefaultUI(this GinkBuilder options)
        {
            options.Services.ConfigureOptions(typeof(GinkUIConfigureOptions));

            return options;
        }
    }
}
