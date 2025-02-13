using BlazorRadzenMls;
using BlazorRadzenMls.Extensions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.AddAuthentication();//extension
builder.AddHttpClients();//extension

builder.Services.AddExternalLibraries();//extension
builder.Services.AddOwnServices();//extension

await builder.Build().RunAsync();