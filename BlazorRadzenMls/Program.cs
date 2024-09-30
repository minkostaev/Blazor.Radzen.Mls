using BlazorRadzenMls;
using BlazorRadzenMls.Contracts;
using BlazorRadzenMls.Extensions;
using BlazorRadzenMls.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOidcAuthentication(options =>
{
    string authConfig = "Authentication:" + AppStatic.GetAuth0(builder.HostEnvironment.BaseAddress);
    builder.Configuration.Bind(authConfig, options.ProviderOptions);//"Auth0"
    options.ProviderOptions.ResponseType = "code";
    ///options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:ClientId"]);
});

///builder.Services.AddHttpClient("ServerAPI",
///    client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
///    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
///builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["API_Prefix"] ?? builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient("ApitoSomee", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Endpoints:ApitoSomee"]!);
    ///client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    ///client.Timeout = TimeSpan.FromMinutes(3);
});///.AddHttpMessageHandler<LocalTokenHandler>();
builder.Services.AddHttpClient("ApitoRender", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Endpoints:ApitoRender"]!);
    ///client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    ///client.Timeout = TimeSpan.FromMinutes(3);
});///.AddHttpMessageHandler<LocalTokenHandler>();
builder.Services.AddScoped<IApitoService, ApitoService>();

builder.Services.AddExternalLibraries();//extension
builder.Services.AddOwnServices();//extension

await builder.Build().RunAsync();