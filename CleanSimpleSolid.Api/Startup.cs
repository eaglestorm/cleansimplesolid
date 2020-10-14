using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using AutoMapper;
using CleanConnect.Common.Model.Settings;
using CleanDdd.Common.Model.Settings;
using CleanSimpleSolid.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServiceBase.Config;
using ServiceBase.Infrastructure;
using ServiceBase.Init;

namespace ServiceBase
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
            // By default, Microsoft has some legacy claim mapping that converts
            // standard JWT claims into proprietary ones. This removes those mappings.

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

            var jwtConfig = Configuration.GetSection("JwtAuthentication");
            services.Configure<JwtAuthentication>(jwtConfig);
            var config = Configuration.GetSection("DatabaseSettings");
            services.Configure<DbSettings>(config);

            //options are configured in Config.ConfigureJwtBearerOptions
            //We only need the authorization bit of the token (the claims) the authentication is not really needed as this is a microservice.
            services.AddAuthentication(options => { options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; })
                .AddJwtBearer();


            services.AddAutoMapper(typeof(InfrastructureProfile), typeof(ApiProfile));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();

            //https is handled by your load balancer, cloudflare, etc.  Should not be handled here

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.Register<JwtAuthentication>(ctx =>
            {
                var section = Configuration.GetSection("JwtAuthentication");
                return section.Get<JwtAuthentication>();
            }).SingleInstance();
            builder.RegisterType<ConfigureJwtBearerOptions>().As<IPostConfigureOptions<JwtBearerOptions>>();
            builder.RegisterType<DbMigrationTask>().As<IStartupTask>();
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterModule(new CoreModule());
        }
    }
}