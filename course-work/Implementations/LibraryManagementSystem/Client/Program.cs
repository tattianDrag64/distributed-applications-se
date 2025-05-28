using Client;  
using ClientLibrary.Services.Implementations;  
using ClientLibrary.Services.Interfaces;  
using Microsoft.AspNetCore.Components.Web;  
using MudBlazor.Services;  
using Microsoft.Extensions.DependencyInjection;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient for static resources (CSS/JS) — base address = client host  
builder.Services.AddScoped(sp =>
  new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// HttpClient for API — base address = your API server  
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7022/");
});

builder.Services.AddMudServices();

// Register IAuthorManager, injecting ApiClient  
//builder.Services.AddScoped<IAuthorManager>(sp =>
//{
//    var httpClient = sp.GetRequiredService<IHttpClientFactory>()
//                       .CreateClient("ApiClient");
//    return new AuthorManager(httpClient);
//});

// Add Blazored LocalStorage  
builder.Services.AddBlazoredLocalStorage();

// Add Authentication and Authorization services  
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

// Register additional managers  
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IBookManager, BookManager>();
builder.Services.AddScoped<IGenreManager, GenreManager>();
builder.Services.AddScoped<ISearchManager, SearchManager>();
builder.Services.AddScoped<IEventManager, EventManager>();
builder.Services.AddScoped<IReservationManager, ReservationManager>();
builder.Services.AddScoped<IBookCopyManager, BookCopyManager>();
builder.Services.AddScoped<IAuthorManager, AuthorManager>();

await builder.Build().RunAsync();
