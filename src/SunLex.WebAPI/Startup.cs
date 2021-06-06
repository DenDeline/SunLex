using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SunLex.Infrastructure;
using SunLex.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SunLex.ApplicationCore;

namespace SunLex.WebAPI
{
    public class Startup
    {
        public const string NextJsClientCorsPolicy = "NextJsClientCorsPolicy";
        
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(config => config.AddPolicy(NextJsClientCorsPolicy, builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            }));
            
            services.AddAutoMapper(typeof(Startup));
            
            services.AddDbContext<AppDbContext>(config => 
                config.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();

            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SunLex.WebAPI", Version = "v1" });
                c.EnableAnnotations();
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new DefaultInfrastructureModule());
            builder.RegisterModule(new DefaultCoreModule());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.DisplayRequestDuration();
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SunLex.WebAPI v1");
                });
            }
            
            app.UseHttpsRedirection();

            app.UseCors(NextJsClientCorsPolicy);
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
