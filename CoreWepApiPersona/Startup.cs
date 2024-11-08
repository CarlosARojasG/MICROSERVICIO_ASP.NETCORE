using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CoreWepApiPersona.Data;

namespace CoreWepApiPersona
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
            services.AddCors();//PERMISO PARA QUE NUESTRA APLICACION DE ANGULAR PUEDA USARLA**********************
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoreWepApiPersona", Version = "v1" });
            });

            services.AddDbContext<CoreWepApiPersonaContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("CoreWepApiPersonaContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options => {//PARA PERMITIR PERMISOS DE SOLICITUDES DE LUGARES EN ESPECIFICO EN ESTE CASO DE NUESTRA APLICACION DE ANGULAR
                                    //options.WithOrigins("http://localhost:4200"); //ANGULAR
                                    //options.WithOrigins("http://localhost:3000"); //REACT
                options.WithOrigins("http://localhost");//PERMISO PARA DOCKER
                options.AllowAnyMethod();
                options.AllowAnyHeader();
                });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoreWepApiPersona v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
