using BlazorRadzenMls;
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

//builder.AddAuthentication();//extension
builder.AddHttpClients();//extension

builder.Services.AddExternalLibraries();//extension
builder.Services.AddOwnServices();//extension

await builder.Build().RunAsync();