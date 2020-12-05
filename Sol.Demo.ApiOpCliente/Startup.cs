using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sol.Demo.ApiOpCliente.Contexto;
using Sol.Demo.ApiOpCliente.Services;
using Sol.Demo.ApiOpCliente.ServicesGrpc;
using Sol.Demo.Comunes.Configs;

namespace Sol.Demo.ApiOpCliente
{
    public class Startup
    {
        public Startup(IConfiguration configuration)// ILogger<Startup> logger)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IdentityServerConfig identityConfig =
               Configuration.GetSection("IdentityServer").Get<IdentityServerConfig>();
            
            string cadena = Configuration.GetValue<string>("ConnectionStrings:BDCliente");
            services.AddDbContext<ClienteContext>(options => {
                options.UseSqlServer(cadena);
            });

            services.AddScoped<IClienteServices, ClienteServices>();
            services.AddScoped<ICuentaServices, CuentaServices>();

            services.AddControllers();
            services.AddGrpc();

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options => {
                    options.Authority = identityConfig.UrlServer;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters =
                        new Microsoft.IdentityModel.Tokens.TokenValidationParameters {
                            ValidateAudience = false,
                            ValidateIssuer = false
                        };
                });

            services.AddAuthorization(opt => {

                opt.AddPolicy("Apiscope", pol => {
                    pol.RequireAuthenticatedUser();
                    pol.RequireClaim("scope", identityConfig.ResourceId);
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env,
            ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            IdentityServerConfig config = Configuration
                      .GetSection("IdentityServer").Get<IdentityServerConfig>();

            logger.LogWarning("Llego IdentityServer " + Newtonsoft.Json.JsonConvert.SerializeObject(config));


            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<BancaServerGrpc>();
                 endpoints.MapControllers().RequireAuthorization("Apiscope");
            });
        }
    }
}
