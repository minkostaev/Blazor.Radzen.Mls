using AKSoftware.Localization.MultiLanguages.Providers;
using AKSoftware.Localization.MultiLanguages;
using BlazorRadzenMls;
using BlazorRadzenMls.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Reflection;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<AppState>();

// https://akmultilanguages.azurewebsites.net/
builder.Services.AddLanguageContainer<EmbeddedResourceKeysProvider>(Assembly.GetExecutingAssembly(), "Languages");
//builder.Services.AddLanguageContainerFromFolder("Languages", CultureInfo.GetCultureInfo("en-US"));

await builder.Build().RunAsync();
