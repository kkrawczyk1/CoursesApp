using System.Linq;
using AutoMapper;
using Courses.Context;
using Courses.Helpers;
using Courses.Startups;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Courses
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureMediatR(services);
            ConfigureSwagger(services);
            AutoFacStartup.RegisterServices(services);
            CQRSStartup.RegisterServices(services);
            ConfigureCors(services);
            ConfigureMvc(services);
            services.AddEntityFrameworkSqlServer();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            var mappingConfig = new MapperConfiguration(cfg =>
                cfg.AddMaps(new[] {
                    "Courses",
                })
            );
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            AddMapper(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseWebSockets();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("MyPolicy");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Courses");
            });
        }
        private static void ConfigureMediatR(IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();
            services.AddTransient<ServiceFactory>(p => p.GetService);
        }

        public static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "WEB API",
                    Description = "API for project Courses",
                });
                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

        }
        private void ConfigureMvc(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddMvc()
                .AddNewtonsoftJson(opts =>
                    opts.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local);
        }
        private static void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(opts => opts.AddPolicy("MyPolicy", builder =>
            {
                builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(host => true)
                    .AllowCredentials();
            }));
        }
        static void AddMapper(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile<AutoMapperConfig>(); });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
