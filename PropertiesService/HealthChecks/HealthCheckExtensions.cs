using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace PropertyService.HealthChecks
{
    public static class HealthCheckExtensions
    {
        public static IEndpointConventionBuilder MapHealthChecks(this IEndpointRouteBuilder builder)
        {
            var options = new HealthCheckOptions
            {
                AllowCachingResponses = false,
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
                    [HealthStatus.Degraded] = StatusCodes.Status503ServiceUnavailable,
                },
            };

            return builder.MapHealthChecks("/health", options);
        }
    }
}