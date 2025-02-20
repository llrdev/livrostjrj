using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;

namespace Webapi.Extensions
{
    /// <summary>
    /// Swagger Extensions
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Injeção de dependencia dos servicos do domínio
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="title"></param>
        /// <param name="version"></param>
        public static void UseSwagger([NotNull] this IServiceCollection serviceCollection, string title, string version)
        {
            serviceCollection.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = title, Version = version, Description = "WebApi (Core 3.1 LTS)" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                    },
                    new string[] { }
                }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}
