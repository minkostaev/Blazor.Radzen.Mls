namespace BlazorRadzenMls.Services;

using BlazorRadzenMls.Contracts;
using BlazorRadzenMls.Extensions;
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
    public async Task CheckVersion()
    {
        string version = await _IJSRuntime.CheckVersion(_navigationManager, this, "CheckVersion");
        
        bool needUpdate = !AppValues.VersionClient.StartsWith(version);
        _appState.VersionServer = version;
        _appState.NeedUpdate = needUpdate;
    }

    /// <summary>
    /// Calls js method that hard reload the site
    /// </summary>
    /// <returns>The success of reload</returns>
    public async Task<bool> Reload()
    {
        return await _IJSRuntime.Reload(this, "Reload");
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