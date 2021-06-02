using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsdown_Authentication_Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            
        }


		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
			.ConfigureAppConfiguration((hostingContext, config) =>
			{
				var env = hostingContext.HostingEnvironment;
				Console.WriteLine($"the environment is now: {env.EnvironmentName}");

				//TODO: hij pakt deze niet goed in kubernetes?
				config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json",
								optional: true, reloadOnChange: true);

			}).ConfigureLogging((context , logging) =>
			{
				logging.ClearProviders();
				logging.AddConfiguration(context.Configuration.GetSection("Logging"));
				logging.AddDebug();

			})
			.ConfigureWebHostDefaults(webBuilder =>
			{
				webBuilder.UseStartup<Startup>();
			});
	}
}
