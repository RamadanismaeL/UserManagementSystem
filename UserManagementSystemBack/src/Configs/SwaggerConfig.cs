/*
*@author Ramadan Ismael
*/

using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UserManagementSystemBack.src.Configs
{
    /// <summary>
    /// Swagger Configuaration
    /// </summary>
    public static class SwaggerConfig
    {
        /// <summary>
        /// Simple config method
        /// </summary>
        /// <param name="service"></param>
        public static void AddSwaggerConfiguration(this IServiceCollection service)
        {
            service.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                ConfigureSwaggerDoc(options);
                ConfigureJwtAuthentication(options);
            });

            static void ConfigureSwaggerDoc(SwaggerGenOptions options)
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "User Management System",
                    Version = "v1",
                    Description = "A simple management system ASP.NET Core Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Admin: Ramadan Ibraimo Ismael",
                        Email = "ramadan.ismael02@gmail.com",
                        Url = new Uri("https://github.com/RamadanismaeL")
                    }
                });
            }

            static void ConfigureJwtAuthentication(SwaggerGenOptions options)
            {
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "Enter JWT Bearer token **_only_**",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securitySchema);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                });
            }
        }

        /// <summary>
        /// Invoke a Swagger interface to test API and show documentation
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "UMS api, v.1");
                options.RoutePrefix = string.Empty;

                options.DocumentTitle = "documentação";
                options.DisplayRequestDuration();
            });
        }
    }
}