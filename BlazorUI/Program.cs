using Blazored.Toast;
using BlazorUI.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            var apiBaseUrl = builder.Configuration["Api:BaseUrl"];

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl!) });

            builder.Services.AddBlazoredToast();

            builder.Services.AddScoped<IApiSvc, ApiSvc>();

            await builder.Build().RunAsync();
        }
    }
}
