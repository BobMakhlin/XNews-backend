using System.Threading.Tasks;
using Application;
using Application.Identity.Interfaces;
using Application.Persistence.Interfaces;
using Application.Validation;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Persistence.Logging;
using Persistence.Primary;
using Presentation.API.Extensions;
using Presentation.API.Middlewares;
using Presentation.Common.Services;

namespace Presentation.API
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
            services.AddCors();
        
            services.AddControllers();
            RegisterSwagger(services);

            services.AddSingleton<ICurrentLoggedInUserService, CurrentLoggedInUserService>();
            services.AddHttpContextAccessor();

            services.AddApplication();
            services.AddApplicationValidation();
            
            services.AddPersistence(Configuration);
            services.AddPersistenceLogging(Configuration);
            
            services.AddIdentityDbContext(Configuration);
            services.AddIdentitySystem(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Presentation.API v1"));
            }

            app.UseCors(Configuration);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        /// <summary>
        /// Registers Swagger in the specified <paramref name="services"/>.
        /// </summary>
        private void RegisterSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Presentation.API", Version = "v1"});

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    In = ParameterLocation.Header
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
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
            });
        }
        
        private async Task SeedDatabaseAsync(IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            
            IDbSeeder seeder = scope.ServiceProvider.GetRequiredService<IDbSeeder>();
            await seeder.SeedAsync();
        }
    }
}