namespace BlazorRadzenMls.Extensions;

using AKSoftware.Localization.MultiLanguages;
using AKSoftware.Localization.MultiLanguages.Providers;
using Blazored.LocalStorage;
using BlazorRadzenMls.Contracts;
using BlazorRadzenMls.Services;
using Radzen;
using System.Globalization;
using System.Reflection;

public static class ProgramServices
{
    public static void AddExternalLibraries(this IServiceCollection services)
    {
        // https://blazor.radzen.com/
        // this is required for: Dialog, Notification, ContextMenu and Tooltip
        services.AddRadzenComponents();

        // https://akmultilanguages.azurewebsites.net/
        ///services.AddLanguageContainer<EmbeddedResourceKeysProvider>(Assembly.GetExecutingAssembly(), "Languages");
        services.AddLanguageContainer(Assembly.GetExecutingAssembly(), CultureInfo.GetCultureInfo("en-US"), "Languages");

        // https://github.com/Blazored/LocalStorage
        services.AddBlazoredLocalStorage();
        ///builder.Services.AddBlazoredLocalStorageAsSingleton();
    }
    public static void AddOwnServices(this IServiceCollection services)
    {
        services.AddScoped<JavaScriptCustom>();
        services.AddScoped<AppState>();
        services.AddScoped<MultiLanguage>();
        services.AddScoped<IRdznTheming, RdznTheming>();
        services.AddScoped<IVersionReload, VersionReload>();
    }
}