namespace BlazorRadzenMls.Services;

using Microsoft.JSInterop;

public static class RadzenTheming
{

    public static async Task<string> GetTheme(IJSRuntime js)
    {
        try { return await js.InvokeAsync<string>("getRadzenTheme"); }
        catch { Console.WriteLine("js method error"); return string.Empty; }
    }

    public static async Task SetTheme(IJSRuntime js, string? val)
    {
        if (!Themes.Contains(val))
            val = Themes[0];
        try { await js.InvokeVoidAsync("setRadzenTheme", val); }
        catch { Console.WriteLine("js method error"); }
    }

    // free radzen themes names
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
//<script src = "js/myRadzen.js" ></ script > -js code is in here