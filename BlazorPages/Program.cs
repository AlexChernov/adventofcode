using System.Threading.Tasks;
using BlazorStrap;
using Microsoft.AspNetCore.Blazor.Hosting;

namespace AdventOfCode
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services.AddBootstrapCss();
            builder.RootComponents.Add<App>("app");

            await builder.Build().RunAsync();
        }
    }
}
