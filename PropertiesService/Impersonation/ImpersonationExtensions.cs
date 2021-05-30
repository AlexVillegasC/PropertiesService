using PropertyService.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PropertyService.Impersonation
{
    public static class ImpersonationExtensions
    {
        public static IServiceCollection AddImpersonation(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var value = configuration.GetSectionValue<ImpersonationOptions>();
            services.Add(new ServiceDescriptor(typeof(IImpersonation), provider => new Impersonation(value), ServiceLifetime.Singleton));
            return services;
        }
    }
}
