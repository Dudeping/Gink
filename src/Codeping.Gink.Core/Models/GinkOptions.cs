using Microsoft.Extensions.DependencyInjection;
using System;

namespace Codeping.Gink.Core
{
    public class GinkOptions
    {
        internal GinkOptions(IServiceCollection services)
        {
            this.Services = services;
        }

        public IServiceCollection Services { get; }
        public int RetryNumWhenConfilict { get; set; } = 10;
    }
}
