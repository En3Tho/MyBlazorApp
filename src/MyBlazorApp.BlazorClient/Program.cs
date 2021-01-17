using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace MyBlazorApp.BlazorClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.ConfigureOptions()
                   .ConfigureLogging()
                   .ConfigureDI();

            await builder.Build().RunAsync();
        }
    }
}