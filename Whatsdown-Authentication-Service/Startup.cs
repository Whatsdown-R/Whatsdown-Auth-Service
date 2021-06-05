using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Authentication_Service.Data;
using Microsoft.AspNetCore.Authentication.OAuth;
using Whatsdown_Authentication_Service.Logic;
using Microsoft.OpenApi.Models;

namespace Whatsdown_Authentication_Service
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
          
       

            services.AddDbContext<AuthenticationContext>(options =>
                 options.UseMySql(Configuration.GetConnectionString("AuthServiceDB")));
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(x => true)
                    .AllowCredentials());
            });
            services.AddLogging();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PostService", Version = "v1" });
            });
            services.AddSingleton<AuthV1Logic>();

         
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
       

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PostService v1"));
            }
           
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
