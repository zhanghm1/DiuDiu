using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiuDiu;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Demo.UserApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var client = services.GetRequiredService<IDiuDiuClient>();
                    var config = services.GetRequiredService<IConfiguration>();
                    string localUrl = config["DiuDiu:LocalUrl"];
                    string HelthCheck = config["DiuDiu:HelthCheck"];
                    string ServiceName = config["DiuDiu:ServiceName"];
                    string ServiceSecret = config["DiuDiu:ServiceSecret"];
                    Uri uri = new Uri(localUrl);
                    DiuDiuService service = new DiuDiuService()
                    {
                        Host= uri.Host,
                        Port= uri.Port,
                        Name= ServiceName,
                        Secret= ServiceSecret,
                        ID = ServiceName+ uri.Host+ uri.Port,
                        Check=new DiuDiuServiceCheck() {
                            Address= HelthCheck,
                            ErrorTimes=5,
                            Interval=10,
                            StartCheckTime=5,
                            TimeOut=5
                        }
                    };

                    client.RegisterService(service).Wait();


                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "DiuDiuService ×¢²áÊ§°Ü");
                }
            }

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
