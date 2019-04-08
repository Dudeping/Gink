using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System;

namespace Codeping.Gink.UI
{
    internal class GinkUIConfigureOptions : IPostConfigureOptions<StaticFileOptions>
    {
        private readonly IWebHostEnvironment _host;

        public GinkUIConfigureOptions(IWebHostEnvironment host)
        {
            _host = host;
        }

        public void PostConfigure(string name, StaticFileOptions options)
        {
            name = name ?? throw new ArgumentNullException(nameof(name));
            options = options ?? throw new ArgumentNullException(nameof(options));

            options.ContentTypeProvider ??= new FileExtensionContentTypeProvider();

            if (options.FileProvider == null && _host.WebRootFileProvider == null)
            {
                throw new InvalidOperationException("Missing FileProvider.");
            }

            options.FileProvider ??= _host.WebRootFileProvider;

            var fileProvider = new ManifestEmbeddedFileProvider(this.GetType().Assembly, "wwwroot");

            options.FileProvider = new CompositeFileProvider(options.FileProvider, fileProvider);
        }
    }
}
