using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using AutoMapper;
using Microsoft.AspNetCore.ResponseCompression;
using DesafioSouthSystem.WebAPI.Mappers;
using System.Reflection;
using DesafioSouthSystem.Shared.Models;
using Microsoft.OpenApi.Models;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;

namespace DesafioSouthSystem.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", false, true)
                    .Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //var appSettings = new AppSettings();
            //Configuration.Bind(appSettings);
            services.Configure<AppSettings>(Configuration);

            services.AddControllers();
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression();

            #region MediatR
            services.AddMediatR(
                AppDomain.CurrentDomain.Load("DesafioSouthSystem.WebAPI"),
                AppDomain.CurrentDomain.Load("DesafioSouthSystem.Domain")
            ); 
            #endregion

            #region AutoMapper
            var config = new MapperConfiguration(cfg =>
               {
                   cfg.AddProfile(new ViewModelToDomainMappingProfile());
               });
            services.AddSingleton(config.CreateMapper());
            #endregion

            #region  SWAGGER
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Desafio South System Web API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() { In = ParameterLocation.Header, Description = "Informe o token JWT com Bearer no campo", Name = "Authorization", Type = SecuritySchemeType.ApiKey });

                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "DesafioSouthSystem.xml");
                c.IncludeXmlComments(filePath);
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region  SWAGGER
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DesafioSouthSystem API V1");
            });
            #endregion

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
