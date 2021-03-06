using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Nordax.Bank.Recruitment
{
    public class Program
    {
        public static void Main(string[] args)
        { 
	        CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
	        return Host.CreateDefaultBuilder(args)
		        .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
		        .UseDefaultServiceProvider((context, options) =>
		        {
			        options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
			        options.ValidateOnBuild = true;
		        });
        }
    }
}