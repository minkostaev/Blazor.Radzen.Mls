namespace BlazorRadzenMls.Services;

using BlazorRadzenMls.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

public static class VersionReload2
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
            Console.WriteLine(param);
            version = await js.InvokeAsync<string>("getVersion", param);
            version = version.Trim();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error with IJSRuntime");
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
    public static async Task Reload(IJSRuntime js)
    {
        try { await js.InvokeVoidAsync("reload"); }
        catch { Console.WriteLine("js reload error"); }
    }

}