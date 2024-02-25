namespace BlazorRadzenMls.Services;

using BlazorRadzenMls.Contracts;
using BlazorRadzenMls.Models;
using Microsoft.JSInterop;

public class RadzenTheming : IRadzenTheming
{
    private readonly IJSRuntime _IJSRuntime;
    private readonly AppState _appState;
    public RadzenTheming(IJSRuntime IJSRuntime, AppState appState)
    {
        _IJSRuntime = IJSRuntime;
        _appState = appState;
        Themes = ["default", "standard", "software", "humanistic", "dark", "material"];
    }

    /// <summary>
    /// Available radzen themes names
    /// </summary>
    public string[] Themes { get; set; }

    /// <summary>
    /// Get current radzen theme name
    /// </summary>
    /// <returns>Theme name</returns>
    public async Task<string> GetTheme()
    {
        #region AppState
        if (!string.IsNullOrEmpty(_appState.SiteOptions.Theme)
            && Themes.Contains(_appState.SiteOptions.Theme))
        {
            return _appState.SiteOptions.Theme;
        }
        #endregion
        try
        {
            return await _IJSRuntime.InvokeAsync<string>("getRadzenTheme");
        }
        catch (Exception ex)
        {
            Console.WriteLine(AppValues.JsErrorString("getRadzenTheme", "GetTheme"));
            Console.WriteLine(ex.Message);
            return string.Empty;
        }
    }

    /// <summary>
    /// Set radzen theme
    /// </summary>
    /// <param name="name">Themes name</param>
    /// <returns>The success of theme change</returns>
    public async Task<bool> SetTheme(string? name, bool saveLocal = false)
    {
        if (!Themes.Contains(name))
        {
            name = Themes.FirstOrDefault();
        }
        bool success;
        try
        {
            await _IJSRuntime.InvokeVoidAsync("setRadzenTheme", name);
            success = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(AppValues.JsErrorString("setRadzenTheme", "SetTheme"));
            Console.WriteLine(ex.Message);
            success = false;
        }
        #region AppState
        _appState.SiteOptions.Theme = name;
        if (saveLocal && success)
        {
            await _appState.SaveAppOptions();
        }
        #endregion
        return success;
    }
}
//requirements:
//@inject IJSRuntime jsRuntime
//function setRadzenTheme(val)
//{
//    let links = document.head.getElementsByTagName('link');
//    for (let li of links)
//    {
//        if (li.href.includes('_content/Radzen.Blazor/css/'))
//        {
//            let css = li.href.split('/').slice(-1)[0];
//            let newLink = li.href.replace(css, val + '-base.css');
//            li.href = newLink;
//        }
//    }
//}
//in index.html
//<link rel="stylesheet" href="_content/Radzen.Blazor/css/default-base.css">
//<script src="js/customRadzen.js"></script> -js code is in here