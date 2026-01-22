using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PuppyStore;
using PuppyStore.Client.Services;
using Blazored.SessionStorage;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

// HttpClient for calling your server API
// HttpClient MUST point to your server API, NOT the client URL
builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Client-side services
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<FavoritesService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<AuthState>();

// Static puppies data
builder.Services.AddSingleton<PuppiesRepository>();

await builder.Build().RunAsync();
