namespace BlazorRadzenMls.Services;

using Microsoft.JSInterop;

public class ThemeChanging
{
    //public Task Initialize { get; }// !!! await !!!
    //public bool Initiated { get; protected set; }// optional
    //public ThemeChanging(IJSRuntime js) { Initialize = GetTheme(js); }//Task.FromResult(GetTheme(js));
    public async Task GetTheme(IJSRuntime js)
    {
        try { Theme = await js.InvokeAsync<string>("getRadzenTheme"); }
        catch { Console.WriteLine("js method error"); }
        //catch { _isDefault = true; Console.WriteLine("js method error"); }
        //Initiated = true;
    }

    public async Task SetTheme(IJSRuntime js, string val)
    {
        Theme = val;
        try { await js.InvokeVoidAsync("setRadzenTheme", val); }
        catch { Console.WriteLine("js method error"); }
    }

    public string[] Themes => ["default", "standard", "software", "humanistic", "dark", "material"];

    public string? Theme { get; set; }

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