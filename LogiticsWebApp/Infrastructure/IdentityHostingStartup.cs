using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(LogiticsWebApp.Infrastructure.IdentityHostingStartup))]
namespace LogiticsWebApp.Infrastructure
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}
