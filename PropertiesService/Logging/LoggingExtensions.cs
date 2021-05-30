using System.Threading;
using PropertyService.Logging.Telemetry;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace PropertyService.Logging
{
    public static class LoggingExtensions
    {
        public static IServiceCollection AddLogging(this IServiceCollection services, IConfiguration configuration)
        {
            var applicationInsightsServiceOptions = new ApplicationInsightsServiceOptions
            {
                EnableEventCounterCollectionModule = false,
                EnablePerformanceCounterCollectionModule = false,
            };
            services
               .AddApplicationInsightsTelemetry(
                    applicationInsightsServiceOptions)
               .AddApplicationInsights(configuration);

            var serviceProvider = services.BuildServiceProvider();
            var client = (TelemetryClient)serviceProvider.GetService(typeof(TelemetryClient));

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.ApplicationInsights(client, TelemetryConverter.Traces)
                .CreateLogger();

            return services;
        }

        public static IHostBuilder UseLogging(this IHostBuilder hostBuilder)
        {
            hostBuilder
                .ConfigureLogging(builder =>
                {
                    // remove default logging providers
                    builder.ClearProviders();
                })
                .UseSerilog();

            return hostBuilder;
        }

        public static IApplicationBuilder UseLogging(this IApplicationBuilder app)
        {
            app.UseMiddleware<ApplicationInsightsLoggingMiddleware>();
            var applicationLifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            applicationLifetime?.ApplicationStopping.Register(() =>
            {
                var telemetryClient = app.ApplicationServices.GetService<TelemetryClient>();
                if (telemetryClient != null)
                {
                    telemetryClient.TrackTrace("Application Stopping");
                    telemetryClient.Flush();
                    Thread.Sleep(5000);
                }
            });

            return app;
        }
    }
}
