using PropertyService.Helpers;
using PropertyService.Models;
using PropertyService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace PropertyService
{
    internal static class RegisterServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IPropertiesModel, PropertiesModel>();
            services.AddTransient<IPropertiesRepository, PropertiesRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IHttpContextTokenFetcher, HttpContextTokenFetcher>();
            services.AddTransient<IPropertiesApi, PropertiesApi>();
            services.AddTransient<IPropertiesClient, PropertiesClient>();
            services.AddTransient<IPropertiesService, PropertiesService>();
            services.AddTransient<ICommandHelper, CommandHelper>();

            return services;
        }
    }
}