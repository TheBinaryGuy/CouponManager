using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace CouponManager.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).UseSetting("https_port", "80").Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
