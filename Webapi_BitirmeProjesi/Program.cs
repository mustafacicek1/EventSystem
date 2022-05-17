using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Webapi_BitirmeProjesi.DbOperations;

namespace Webapi_BitirmeProjesi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            DataGenerator.Initialize();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
