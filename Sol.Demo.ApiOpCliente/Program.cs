using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Sol.Demo.ApiOpCliente
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //      WebHost.CreateDefaultBuilder(args)
        //      .ConfigureAppConfiguration((hostingContext, config) =>
        //      {
        //          IConfigurationRoot configuration
        //              = new ConfigurationBuilder()
        //                  .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        //                  .Build();
        //          config.AddEnvironmentVariables();
        //      }).UseStartup<Startup>();
        
    }
}
