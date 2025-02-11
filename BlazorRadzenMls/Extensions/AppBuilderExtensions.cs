namespace BlazorRadzenMls.Extensions;

using BlazorRadzenMls.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Headers;

public static class AppBuilderExtensions
{
    public static void AddAuthentication(this WebAssemblyHostBuilder builder)
    {
        ///builder.Services.AddOidcAuthentication(options =>
        ///{
        ///    options.ProviderOptions.Authority = builder.Configuration["Authentication:Auth0:Authority"];
        ///    options.ProviderOptions.ClientId = builder.Configuration["Authentication:Auth0:ClientId"];
        ///    options.ProviderOptions.ResponseType = "token id_token";
        ///    options.ProviderOptions.DefaultScopes.Add("openid");
        ///    options.ProviderOptions.DefaultScopes.Add("profile");
        ///    options.ProviderOptions.DefaultScopes.Add("email");
        ///});
        builder.Services.AddOidcAuthentication(options =>
        {
            string authConfig = "Authentication:" + AppStatic.GetAuth0(builder.HostEnvironment.BaseAddress);
            builder.Configuration.Bind(authConfig, options.ProviderOptions);//"Auth0"
            options.ProviderOptions.ResponseType = "code";
            options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Authentication:Auth0:Audience"]!);
        });
    }
    
    public static void AddHttpClients(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        builder.Services.AddHttpClient("ApitoSomee", client =>
        {
            client.BaseAddress = new Uri(builder.Configuration["Endpoints:ApitoSomee"]!);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromMinutes(3);
        }).AddHttpMessageHandler<AuthMessageHandler>();
        builder.Services.AddHttpClient("ApitoRender", client =>
        {
            client.BaseAddress = new Uri(builder.Configuration["Endpoints:ApitoRender"]!);
            ///client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ///client.Timeout = TimeSpan.FromMinutes(3);
        });///.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
    }
}