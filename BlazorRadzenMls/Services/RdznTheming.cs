namespace BlazorRadzenMls.Services;

using BlazorRadzenMls.Contracts;
using BlazorRadzenMls.Extensions;
using Microsoft.JSInterop;

public class RdznTheming(IJSRuntime iJSRuntime, AppState appState) : IRdznTheming
{
    private readonly IJSRuntime __js = iJSRuntime;
    private readonly AppState __state = appState;

    /// <summary>
    /// Available radzen themes names
    /// </summary>
    public string[] Themes { get; private set; } =
        ["default", "standard", "software", "humanistic", "dark", "material"];

    /// <summary>
    /// Get current radzen theme name
    /// </summary>
    /// <returns>Theme name</returns>
    public async Task<string?> GetTheme()
    {
        string? name = null;
        try { name = __state.SiteOptions.Theme; }
        catch (Exception) { }
        if (!string.IsNullOrEmpty(name) && Themes.Contains(name))
        {
            return name;
        }
        name = await __js.GetTheme(this, "GetTheme");
        if (!string.IsNullOrEmpty(name) && Themes.Contains(name))
        {
            return name;
        }
        return Themes.FirstOrDefault();
    }

    /// <summary>
    /// Set radzen theme
    /// </summary>
    /// <param name="name">Themes name</param>
    /// <param name="saveLocal">Save to local storage</param>
    /// <returns>The success of theme change</returns>
    public async Task<bool> SetTheme(string? name, bool saveLocal = false)
    {
        if (!Themes.Contains(name))
        {
            name = Themes.FirstOrDefault();
        }
        bool success = await __js.SetTheme(name, this, "SetTheme");
        __state.SiteOptions.Theme = name;
        if (saveLocal && success)
        {
            await __state.SaveAppOptions();
        }
        return success;
    }
}
//requirements: in index.html
//<link rel="stylesheet" href="_content/Radzen.Blazor/css/default-base.css"> - theme name
//<script src="js/customRadzen.js"></script> - js code is in here