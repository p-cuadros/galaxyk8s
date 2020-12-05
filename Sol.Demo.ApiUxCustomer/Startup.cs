using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sol.Demo.ApiUxCustomer.Servicios;
using Sol.Demo.ApiUxCustomer.Helpers;
using Sol.Demo.Comunes.Configs;

namespace Sol.Demo.ApiUxCustomer
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
            string valor = Configuration.GetValue<string>("UrlApiCliente");
            ///logger.LogError("Llego UrlApiCliente " + valor);
            IdentityServerConfig config = Configuration
                .GetSection("IdentityServer").Get<IdentityServerConfig>();

            services.Configure<IdentityServerConfig>
                (Configuration.GetSection("IdentityServer"));

            //Usado para poder generar los Transient dinamicamente
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddTransient<ITokenAdapter, TokenAdapter>();

            services.AddTransient<ICustomerServices>(serviceProvider =>
            {
                var context = serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext;
                var header = context.Request.Headers["protocol"];
                if (string.IsNullOrEmpty(header) || header == "http")
                {
                    return new CustomerHttpServices
                        (serviceProvider.GetService<IConfiguration>());
                }
                else
                {
                    return new CustomerGrpcServices(
                        serviceProvider.GetService<ITokenAdapter>(),
                        serviceProvider.GetService<IConfiguration>()
                        );
                }
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
