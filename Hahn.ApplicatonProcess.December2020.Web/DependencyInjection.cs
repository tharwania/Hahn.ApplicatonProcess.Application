using Hahn.ApplicatonProcess.December2020.Data.Context;
using Hahn.ApplicatonProcess.December2020.Data.UnitOfWork;
using Hahn.ApplicatonProcess.December2020.Domain.Applicant;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Hahn.ApplicatonProcess.December2020.Web
{
    public static class DependencyInjection
    {
        public static void AddDataLayerDepencies(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(
               options => options.UseInMemoryDatabase(databaseName: "Applicant"));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }


        public static void AddDomainLayerDepencies(this IServiceCollection services)
        {
            services.AddScoped<IApplicantService, ApplicantService>();
        }
        public static void AddLocalizations(this IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.SetDefaultCulture("en");
                options.AddSupportedUICultures("en-US", "de-DE");
            });
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Hahn.ApplicatonProcess.December2020.Web",
                    Description = "A simple ASP.NET Core Web API for Applicants",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Avinash Kumar",
                        Email = "tharwania@gmail.com",
                    },
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });
        }
    }
}
