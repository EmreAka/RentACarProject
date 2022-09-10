global using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using WebClient;
using WebClient.Services;

namespace WebClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddScoped(sp =>
                new HttpClient
                {
                    //BaseAddress = new Uri("https://rentacarbackend.herokuapp.com/api/")
                    BaseAddress = new Uri("http://localhost:56305/api/")
                });
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Outlined;
            });
            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            builder.Services.AddScoped<CarService>();
            builder.Services.AddScoped<BrandService>();
            builder.Services.AddScoped<ColourService>();
            builder.Services.AddScoped<EngineService>();
            builder.Services.AddScoped<FuelService>();

            builder.Services.AddAuthorizationCore();

            await builder.Build().RunAsync();
        }
    }
}