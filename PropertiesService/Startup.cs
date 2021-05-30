﻿using System.Text.Json.Serialization;
using AutoMapper;
using PropertyService;
using PropertyService.Authorization;
using PropertyService.Configuration;
using PropertyService.Impersonation;
using PropertyService.Logging;
using PropertyService.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ConfigurationExtensions = Microsoft.Extensions.Configuration.ConfigurationExtensions;

namespace PropertyService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(Configuration);
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            services.AddHttpContextAccessor();
            services.AddAuthorization(Configuration);
            services.AddImpersonation(Configuration);

            services
                .AddControllers()
                .AddValidation()

                // needed for swagger
                .ConfigureApiBehaviorOptions(options => options.SuppressMapClientErrors = true)
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });

            services.AddApiVersioning(o => o.ReportApiVersions = true);
            services.AddApiBehavior(Configuration);
            services.AddSwagger(Configuration);
            services.AddAutoMapper();
            services.AddConfiguration(Configuration);
            services.AddServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        var logger = loggerFactory.CreateLogger<ILogger<Startup>>();
                        const string faultMessage = "An unexpected fault happened. Try again later.";
                        logger.LogError(error.Error, faultMessage);

                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync(faultMessage);
                    });
                });
            }

            app.UseSwagger(env, Configuration, provider);
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            // Place custom middleware after security...
            app.UseLogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
