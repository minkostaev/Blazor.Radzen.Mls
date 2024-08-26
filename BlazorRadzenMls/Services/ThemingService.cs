﻿namespace BlazorRadzenMls.Services;

using BlazorRadzenMls.Contracts;
using BlazorRadzenMls.Extensions;

public class ThemingService(IJavaScriptService iJSRuntime) : IThemingService
{
    private readonly IJavaScriptService __js = iJSRuntime;

    private string DefaultTheme => ThemesAll.FirstOrDefault()!;
    private bool ThemeExist(string name) => ThemesAll.Contains(name);

    /// <summary>
    /// All available radzen themes names
    /// </summary>
    public string[] ThemesAll { get; private set; } =
        ["default", "dark", "standard", "standard-dark", "software", "software-dark",
         "humanistic", "humanistic-dark", "material", "material-dark"];

    /// <summary>
    /// Radzen themes names without dark names
    /// </summary>
    public string[] ThemesBasic { get; private set; } =
        ["default", "standard", "software", "humanistic", "material"];

    /// <summary>
    /// Use dark theme version (if you left it to null use ThemesAll list else ThemesBasic list)
    /// </summary>
    public bool? IsDark { get; set; }

    /// <summary>
    /// Get current radzen theme name
    /// </summary>
    /// <returns>Theme name</returns>
    public async Task<string?> GetTheme(StateService? __state = null)
    {
        string? name;
        try { name = __state!.SiteOptions.Theme; }
        catch { name = string.Empty; }
        if (!string.IsNullOrEmpty(name) && ThemeExist(name))
        {
            return name;
        }
        name = await __js.GetTheme(this, "GetTheme");
        if (!string.IsNullOrEmpty(name) && ThemeExist(name))
        {
            return name;
        }
        name = DefaultTheme;
        return name;
    }

    /// <summary>
    /// Set radzen theme
    /// </summary>
    /// <param name="name">Themes name</param>
    /// <param name="saveLocal">Save to local storage</param>
    /// <returns>The success of theme change</returns>
    public async Task<bool> SetTheme(string? name, StateService? __state = null, bool saveLocal = false)
    {
        if (!ThemeExist(name!))
        {
            name = DefaultTheme;
        }
        name = CheckForDarkTheme(name!);
        bool success = await __js.SetTheme(name, this, "SetTheme");
        if (__state != null)
        {
            __state!.SiteOptions.Theme = name;
            if (saveLocal && success)
            {
                await __state.SaveAppOptions();
            }
        }
        return success;
    }

    /// <summary>
    /// Use this method if you'll use ThemesBasic list
    /// </summary>
    public async Task UseIsDarkLightProperty(StateService? __state = null)
    {
        string? name = await GetTheme(__state);
        IsDark = name!.Contains("dark");
    }

    /// <summary>
    /// Checks if it uses dark property and uses the needed name
    /// </summary>
    /// <param name="themeName">Theme name</param>
    /// <returns>Name from ThemesBasic list</returns>
    public string CheckForDarkTheme(string themeName)
    {
        if (IsDark == null)
        {
            return themeName;
        }
        if (IsDark == true && themeName == "default")
        {
            return "dark";
        }
        if (IsDark == false && themeName == "dark")
        {
            return "default";
        }
        if (IsDark == true && !themeName.Contains("dark"))
        {
            return themeName + "-dark";
        }
        if (IsDark == false && themeName.Contains("dark"))
        {
            return themeName.Replace("-dark", "");
        }
        return themeName;
    }

    
    public string? ThemeNameAlwaysLight(string? themeName)
    {
        if (IsDark == null || string.IsNullOrEmpty(themeName))
        {
            return themeName;
        }
        if (themeName == "dark")
        {
            return "default";
        }
        return themeName.Replace("-dark", "");
    }

}
///requirements: in index.html
///<link rel="stylesheet" href="_content/Radzen.Blazor/css/default-base.css"> - theme name
///<script src="js/customRadzen.js"></script> - js code is in here