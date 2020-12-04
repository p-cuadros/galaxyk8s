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

namespace Sol.Demo.ApiOpCliente
{
    public class Startup
    {
        //private readonly ILogger<Startup> logger;

        public Startup(IConfiguration configuration)// ILogger<Startup> logger)
        {
            Configuration = configuration;
           // this.logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            //string cnn = Environment.GetEnvironmentVariable("ConnectionStrings_BDCliente");
            string cadena = Configuration.GetValue<string>("ConnectionStrings:BDCliente");
            /*if (string.IsNullOrEmpty(cnn))
            {
                cnn = Configuration.GetValue<string>("ConnectionStrings:BDCliente");
                Console.WriteLine($"Cadena de conexion desa {cnn}");
            }
            else
            {
                Console.WriteLine($"Cadena de conexion emv {cnn}");
            }*/

            services.AddDbContext<ClienteContext>(options =>
            {
                options.UseSqlServer(cadena);
            });

            services.AddScoped<IClienteServices, ClienteServices>();
            services.AddScoped<ICuentaServices, CuentaServices>();

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
