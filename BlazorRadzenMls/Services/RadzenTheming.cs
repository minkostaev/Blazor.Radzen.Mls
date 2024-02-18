namespace BlazorRadzenMls.Services;

using BlazorRadzenMls.Models;
using Microsoft.JSInterop;

public static class RadzenTheming
{
    /// <summary>
    /// Get current radzen theme name
    /// </summary>
    /// <param name="js">JS Runtime injection</param>
    /// <returns></returns>
    public static async Task<string> GetTheme(IJSRuntime js)
    {
        try
        {
            return await js.InvokeAsync<string>("getRadzenTheme");
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
    /// <param name="js">JS Runtime injection</param>
    /// <param name="val">theme name</param>
    /// <returns></returns>
    public static async Task<bool> SetTheme(IJSRuntime js, string? val)
    {
        if (!Themes.Contains(val))
        {
            val = Themes[0];
        }
        try
        {
            await js.InvokeVoidAsync("setRadzenTheme", val);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(AppValues.JsErrorString("setRadzenTheme", "SetTheme"));
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    // available radzen themes names
    public static string[] Themes => ["default", "standard", "software", "humanistic", "dark", "material"];
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