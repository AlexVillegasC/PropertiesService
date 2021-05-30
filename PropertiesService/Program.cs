using System;
using System.Threading;
using System.Threading.Tasks;
using PropertyService.CloudFoundry;
using PropertyService.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace PropertyService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using var cancellationTokenSource = new CancellationTokenSource();
            try
            {
                await BuildHost(args)
                   .RunAsync(cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                // ignored, The operation was canceled.
            }
        }

        public static IHost BuildHost(string[] args)
        {
            return CreateHostBuilder(args).Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
            .UseLogging()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UrlsBuilder();
            });
    }
}
