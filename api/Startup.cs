using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace api
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
            services.AddControllers();

            // IdentityServer
            services.AddAuthentication(Configuration["Identity:Scheme"])
                        .AddIdentityServerAuthentication(options =>
                        {
                            options.RequireHttpsMetadata = false; //�O�_�ݭnhttps
                            options.Authority = $"https://{Configuration["Identity:IP"]}:{Configuration["Identity:Port"]}";  //IdentityServer���v���|
                            options.ApiName = Configuration["Service:Name"];  //�ݭn���v���A�ȦW��
                            options.RoleClaimType = "role"; //Add roles
                            options.JwtValidationClockSkew = TimeSpan.FromSeconds(0); // �]�w���Үɶ�����, �L�n�w�]��������, �pToken Life�ɶ��p��5�����аO�o�b�o�̳]�w
                        });
            //CORS
            services.AddCors(options =>
            {
                // CorsPolicy �O�ۭq�� Policy �W��
                options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            //CORS
            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthentication();
            //IdentityServer
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
