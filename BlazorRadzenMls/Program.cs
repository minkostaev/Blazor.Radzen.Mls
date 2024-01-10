using AKSoftware.Localization.MultiLanguages;
using AKSoftware.Localization.MultiLanguages.Providers;
using Radzen;
using BlazorRadzenMls;
using BlazorRadzenMls.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Reflection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//https://blazor.radzen.com/get-started
//this is required for: Dialog, Notification, ContextMenu and Tooltip
builder.Services.AddRadzenComponents();

// https://akmultilanguages.azurewebsites.net/
builder.Services.AddLanguageContainer<EmbeddedResourceKeysProvider>(Assembly.GetExecutingAssembly(), "Languages");
//builder.Services.AddLanguageContainerFromFolder("Languages", CultureInfo.GetCultureInfo("en-US"));

builder.Services.AddScoped<AppState>();

await builder.Build().RunAsync();

// to see
//https://blazor-university.com/

// Built-in Components:
// App
// Router
// DynamicComponent
// ErrorBoundary
// NavMenu
// NavLink

// EditForm
