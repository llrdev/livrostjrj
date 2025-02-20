using Domain;
using FluentValidation.AspNetCore;
using Infra.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;
using Webapi.Extensions;
using Webapi.Filters;
using Webapi.Middleware;

namespace webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            services.UseAutoMapper();
            services.UseDomainServices();
            services.UseUriHelper();
            services.AddCors();
            services.AddControllers().ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
            services.AddHttpContextAccessor();

            services.AddMvc(options => options.Filters.Add(typeof(ModelStateValidatorFilter)))
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<Startup>();
                fv.DisableDataAnnotationsValidation = true;
            });


            services.UseSwagger(title: Configuration.GetValue<string>("Title"), version: Configuration.GetValue<string>("version"));
            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.ToString());
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseDeveloperExceptionPage();
            if (env.IsDevelopment())
            {
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("../swagger/v1/swagger.json", $"{Configuration.GetValue<string>("Title")} v1"));
            app.UseCors("AllowAllOrigins");
            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(options);
            app.UseStaticFiles();

           
        }
    }
}
