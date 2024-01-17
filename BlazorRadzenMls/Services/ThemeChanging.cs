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