namespace BlazorRadzenMls.Models;

using System.Reflection;

public static class AppValues
{
    private static string? Version => Assembly.GetExecutingAssembly().GetName().Version?.ToString();
    public static string VersionClient => (string.IsNullOrEmpty(Version)) ? "0.0.0.0" : Version;

    public const string PageIcons = "Icons";
    public const string PageDogs = "Dogs";
    public const string PageOptions = "Options";

    public static string GitHubMy => "https://github.com/minkostaev/Blazor.Radzen.Mls";
    public static string GitHubRadzen => "https://github.com/radzenhq/radzen-blazor";
    public static string GitHubAKSoftware => "https://github.com/aksoftware98/multilanguages";
    public static string GitHubBlazored => "https://github.com/Blazored";
    public static string GitHubAuth0 => "https://github.com/auth0";

    //public const string RolesAll = "admin, manager, user";
    //public const string RolesManager = "admin, manager";
    //public const string RolesAdmin = "admin";

}
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

//StateHasChanged();
//await InvokeAsync(StateHasChanged);