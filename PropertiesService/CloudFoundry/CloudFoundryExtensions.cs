using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace PropertyService.CloudFoundry
{
    [ExcludeFromCodeCoverage]
    public static class CloudFoundryExtensions
    {
        public static IWebHostBuilder UrlsBuilder(this IWebHostBuilder hostBuilder)
        {
            if (!CfAppEnvironment.IsEnabled)
            {
                return hostBuilder;
            }

            var urls = CfAppEnvironment.ApplicationUrls.Select(s => $"{s}:{CfAppEnvironment.Port}");

            hostBuilder.UseUrls(urls.ToArray());

            return hostBuilder;
        }
    }
}