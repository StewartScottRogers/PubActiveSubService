using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace PubActiveSubService {
    public static class Program {
        public static void Main() =>
            CreateWebHostBuilder().Build().Run();

        public static IWebHostBuilder CreateWebHostBuilder() =>
            WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>();
    }
}
