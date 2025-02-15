namespace BlazorRadzenMls.Extensions;

using BlazorRadzenMls.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Headers;

public static class AppBuilderExtensions
{
    public static void AddAuthentication(this WebAssemblyHostBuilder builder)
    {
        string authConfig = AppStatic.GetAuth0(builder.HostEnvironment.BaseAddress);
        string? audience = builder.Configuration[$"Authentication:{authConfig}:Audience"];
        builder.Services.AddOidcAuthentication(options =>
        {
            builder.Configuration.Bind($"Authentication:{authConfig}", options.ProviderOptions);
            options.ProviderOptions.ResponseType = "code";
            if (!string.IsNullOrWhiteSpace(audience))
                options.ProviderOptions.AdditionalProviderParameters.Add("audience", audience);
        });
        ///builder.Services.AddOidcAuthentication(options =>
        ///{
        ///    options.ProviderOptions.Authority = builder.Configuration["Authentication:Auth0:Authority"];
        ///    options.ProviderOptions.ClientId = builder.Configuration["Authentication:Auth0:ClientId"];
        ///    options.ProviderOptions.ResponseType = "token id_token";
        ///    options.ProviderOptions.DefaultScopes.Add("openid");
        ///    options.ProviderOptions.DefaultScopes.Add("profile");
        ///    options.ProviderOptions.DefaultScopes.Add("email");
        ///});
    }

    public static void AddHttpClients(this WebAssemblyHostBuilder builder)
    {
        var jsonHeader = new MediaTypeWithQualityHeaderValue("application/json");
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        builder.Services.AddHttpClient("ApitoSomee", client =>
        {
            client.BaseAddress = new Uri(builder.Configuration["Endpoints:ApitoSomee"]!);
            client.DefaultRequestHeaders.Accept.Add(jsonHeader);
            client.Timeout = TimeSpan.FromMinutes(3);
        }).AddHttpMessageHandler<AuthMessageHandler>();
        builder.Services.AddHttpClient("ApitoRender", client =>
        {
            client.BaseAddress = new Uri(builder.Configuration["Endpoints:ApitoRender"]!);
            client.DefaultRequestHeaders.Accept.Add(jsonHeader);
            client.Timeout = TimeSpan.FromMinutes(3);
        }).AddHttpMessageHandler<AuthMessageHandler>();
    }
}