namespace BlazorRadzenMls.Services;

using BlazorRadzenMls.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

public static class VersionReload
{
    /// <summary>
    /// Calls js method that returns version number and determine whether is new or not
    /// </summary>
    /// <param name="js">JS Runtime injection</param>
    /// <param name="nav">NavigationManager injection</param>
    /// <returns></returns>
    public static async Task<(string, bool)> IsVersionNew(IJSRuntime js, NavigationManager nav)
    {
        string version = "0.0.0.0";
        try
        {
            string param = AppValues.GetGitHubSub(nav) + "/data/version.txt";
            version = await js.InvokeAsync<string>("fetchText", param);
            version = version.Trim();
        }
        catch (Exception ex)
        {
            Console.WriteLine(AppValues.JsErrorString("fetchText", "IsVersionNew"));
            Console.WriteLine(ex.Message);
        }
        bool needUpdate = !AppValues.VersionClient.StartsWith(version);
        return (version, needUpdate);
    }

    /// <summary>
    /// Calls js method that hard reload the site
    /// </summary>
    /// <param name="js">JS Runtime injection</param>
    /// <returns></returns>
    public static async Task<bool> Reload(IJSRuntime js)
    {
        try
        {
            await js.InvokeVoidAsync("reload");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(AppValues.JsErrorString("reload", "Reload"));
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}