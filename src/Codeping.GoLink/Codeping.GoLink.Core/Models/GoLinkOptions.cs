using Microsoft.Extensions.DependencyInjection;
using System;

namespace Codeping.GoLink.Core
{
    public class GoLinkOptions
    {
        internal GoLinkOptions(IServiceCollection services)
        {
            this.Services = services;
        }

        public IServiceCollection Services { get; }
        public int RetryNumWhenConfilict { get; set; } = 10;
    }
}
