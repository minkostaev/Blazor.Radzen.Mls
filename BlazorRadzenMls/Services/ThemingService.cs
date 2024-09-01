namespace BlazorRadzenMls.Services;

using BlazorRadzenMls.Contracts;
using BlazorRadzenMls.Extensions;

public class ThemingService(IJavaScriptService iJSRuntime) : IThemingService
{
    private readonly IJavaScriptService __js = iJSRuntime;

    private string DefaultTheme => ThemesAll.FirstOrDefault()!;
    
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
    /// Checking is the name of the theme whether is in the list
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool ThemeExist(string name) => ThemesAll.Contains(name);

    /// <summary>
    /// Get current radzen theme name
    /// </summary>
    /// <returns>Theme name</returns>
    public async Task<string> GetTheme()
    {
        string name = string.Empty;
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
    public async Task<string> SetTheme(string? name)
    {
        if (!ThemeExist(name!))
        {
            name = DefaultTheme;
        }
        name = CheckForDarkTheme(name!);
        await __js.SetTheme(name, this, "SetTheme");
        return name;
    }

    /// <summary>
    /// Use this method if you'll use ThemesBasic list
    /// </summary>
    public async Task UseIsDarkLightProperty()
    {
        string? name = await GetTheme();
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="themeName"></param>
    /// <returns></returns>
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