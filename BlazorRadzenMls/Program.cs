using AKSoftware.Localization.MultiLanguages;
using Blazored.LocalStorage;
using BlazorRadzenMls;
using BlazorRadzenMls.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using System.Globalization;
using System.Reflection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOidcAuthentication(options =>
{
    if (builder.HostEnvironment.BaseAddress.Contains("localhost"))
    {
        builder.Configuration.Bind("Auth0", options.ProviderOptions);
    }
    else if (builder.HostEnvironment.BaseAddress.Contains(".azurestaticapps.net"))
    {
        builder.Configuration.Bind("Auth0Azure", options.ProviderOptions);
    }
    else if (builder.HostEnvironment.BaseAddress.Contains(".github.io"))
    {
        builder.Configuration.Bind("Auth0GitHub", options.ProviderOptions);
    }
    else if (builder.HostEnvironment.BaseAddress.Contains(".onrender.com"))
    {
        builder.Configuration.Bind("Auth0Render", options.ProviderOptions);
    }
    else if (builder.HostEnvironment.BaseAddress.Contains(".netlify.app"))
    {
        builder.Configuration.Bind("Auth0Netlify", options.ProviderOptions);
    }
    //
    options.ProviderOptions.ResponseType = "code";
    //options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:ClientId"]);
});

//builder.Services.AddHttpClient("ServerAPI",
//    client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
//    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["API_Prefix"] ?? builder.HostEnvironment.BaseAddress) });

// https://blazor.radzen.com/
// this is required for: Dialog, Notification, ContextMenu and Tooltip
builder.Services.AddRadzenComponents();

// https://akmultilanguages.azurewebsites.net/
//builder.Services.AddLanguageContainer<EmbeddedResourceKeysProvider>(Assembly.GetExecutingAssembly(), "Languages");
builder.Services.AddLanguageContainer(Assembly.GetExecutingAssembly(), CultureInfo.GetCultureInfo("en-US"), "Languages");

// https://github.com/Blazored/LocalStorage
builder.Services.AddBlazoredLocalStorage();
//builder.Services.AddBlazoredLocalStorageAsSingleton();

builder.Services.AddScoped<AppState>();

await builder.Build().RunAsync();
