using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace PropertyService.Validators
{
    public static class ValidationExtensions
    {
        public static IMvcBuilder AddValidation(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddFluentValidation(configuration =>
                configuration.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

            return mvcBuilder;
        }
    }
}