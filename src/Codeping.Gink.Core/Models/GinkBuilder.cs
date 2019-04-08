using Microsoft.Extensions.DependencyInjection;

namespace Codeping.Gink.Core
{
    public class GinkBuilder
    {
        internal GinkBuilder(IServiceCollection services)
        {
            this.Services = services;
        }

        public IServiceCollection Services { get; }
    }
}
