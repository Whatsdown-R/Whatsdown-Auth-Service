using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
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
			var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
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

			})
			.ConfigureWebHostDefaults(webBuilder =>
			{
				webBuilder.UseStartup<Startup>();
			}).ConfigureLogging(logging =>  

	  {
			// clear default logging providers
			logging.ClearProviders();
			
			// add built-in providers manually, as needed 
			logging.AddConsole();
			logging.AddDebug();
		
		});//;
	}
}
