using Client;
using ClientLibrary.Services.Implementations;
using ClientLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Microsoft.Extensions.DependencyInjection; // Add this using directive for AddHttpClient extension method  

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient дл€ статических ресурсов (CSS/JS) Ч базовый адрес = хост клиента  
builder.Services.AddScoped(sp =>
   new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// HttpClient дл€ API Ч базовый адрес = ваш API-сервер  
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7022/");
});

builder.Services.AddMudServices();

// –егистрируем IAuthorManager, инжектиру€ именно ApiClient  
builder.Services.AddScoped<IAuthorManager>(sp =>
{
    var httpClient = sp.GetRequiredService<IHttpClientFactory>()
                       .CreateClient("ApiClient");
    return new AuthorManager(httpClient);
});

await builder.Build().RunAsync();
