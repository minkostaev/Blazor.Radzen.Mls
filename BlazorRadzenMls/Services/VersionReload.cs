namespace BlazorRadzenMls.Services;

using BlazorRadzenMls.Contracts;
using BlazorRadzenMls.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

public class VersionReload : IVersionReload
{
    private readonly IJSRuntime _IJSRuntime;
    private readonly AppState _appState;
    private readonly NavigationManager _navigationManager;
    public VersionReload(IJSRuntime IJSRuntime, AppState appState, NavigationManager navigationManager)
    {
        _IJSRuntime = IJSRuntime;
        _appState = appState;
        _navigationManager = navigationManager;
        _appState = appState;
    }

    /// <summary>
    /// Calls js method that checks version number and determine whether is new or not
    /// </summary>
    /// <returns>The success status of the method</returns>
    public async Task<bool> CheckVersion()
    {
        string version = "0.0.0.0";
        bool success;
        try
        {
            string param = AppValues.GetGitHubSub(_navigationManager) + "/data/version.txt";
            version = await _IJSRuntime.InvokeAsync<string>("fetchText", param);
            version = version.Trim();
            success = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(AppValues.JsErrorString("fetchText", "IsVersionNew"));
            Console.WriteLine(ex.Message);
            success = false;
        }
        #region AppState
        bool needUpdate = !AppValues.VersionClient.StartsWith(version);
        _appState.VersionServer = version;
        _appState.NeedUpdate = needUpdate;
        #endregion
        return success;
    }

    /// <summary>
    /// Calls js method that hard reload the site
    /// </summary>
    /// <returns>The success of reload</returns>
    public async Task<bool> Reload()
    {
        try
        {
            await _IJSRuntime.InvokeVoidAsync("reload");
            ///_navigationManager.Refresh(true);
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
// for version check use js fetch text in /data/version.txt
// version.txt is auto created with pre build script
//async function reload()
//{
//    const keys = await caches.keys();
//    for (let cch of keys) {
//        await caches.delete(cch);
//    }
//    location.replace(location.href);
//}