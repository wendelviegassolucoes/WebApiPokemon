using Microsoft.AspNetCore.Hosting;
using System.Security.Authentication;

namespace WebApiConsole.AppStart
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(web_builder =>
            {
                web_builder.ConfigureKestrel(server_options =>
                {
                    server_options.ConfigureHttpsDefaults(listen_options =>
                    {
                        listen_options.SslProtocols = SslProtocols.Tls12;
                    });

                    server_options.Limits.MinRequestBodyDataRate = null;
                    server_options.Limits.MinResponseDataRate = null;
                    server_options.Limits.MaxRequestBodySize = null;
                });

                web_builder.UseContentRoot(Directory.GetCurrentDirectory());
                web_builder.UseStartup<Startup>();
            });
    }
}