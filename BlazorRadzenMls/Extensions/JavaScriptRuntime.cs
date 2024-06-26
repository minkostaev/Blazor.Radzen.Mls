namespace BlazorRadzenMls.Extensions;

using BlazorRadzenMls.Models;
using Microsoft.JSInterop;

public static class JavaScriptRuntime
{

    #region Error showing on crash
    private static string Error => "JS error: ";
    private static string ComponentName(object component) => component.GetType().Name;
    private static void CatchException(object thisComponent, string cMethod, string jsMethod, string exMessage)
    {
        string name = ComponentName(thisComponent);
        Console.WriteLine($"{Error}{name}.razor -> {cMethod} | {jsMethod}");
        //Console.WriteLine(exMessage);
    }
    #endregion

    #region Uses default js methods

    public static async Task<bool> CopyToClipboard(this IJSRuntime js, string copy, object thisComponent, string methodName = "")
    {
        try
        {
            await js.InvokeVoidAsync("navigator.clipboard.writeText", copy);
            return true;
        }
        catch (Exception ex)
        {
            CatchException(thisComponent, methodName, "navigator.clipboard.writeText", ex.Message);
            return false;
        }
    }

    public static async Task<bool> SessionClear(this IJSRuntime js, object thisComponent, string methodName = "")
    {
        try
        {
            await js.InvokeVoidAsync("sessionStorage.clear");
            return true;
        }
        catch (Exception ex)
        {
            CatchException(thisComponent, methodName, "sessionStorage.clear", ex.Message);
            return false;
        }
    }

    public static async Task<bool> OpenNewTab(this IJSRuntime js, string url, object thisComponent, string methodName = "")
    {
        try
        {
            await js.InvokeVoidAsync("open", url, "_blank");
            return true;
        }
        catch (Exception ex)
        {
            CatchException(thisComponent, methodName, "open_blank", ex.Message);
            return false;
        }
    }

    #endregion

    #region Uses custom coded methods in a external js file

    public static async Task<bool?> IsMobileDevice(this IJSRuntime js, object thisComponent, string methodName = "")
    {
        string jsMethod = "isMobileDevice";
        try
        {
            return await js.InvokeAsync<bool>(jsMethod);
        }
        catch (Exception ex)
        {
            CatchException(thisComponent, methodName, jsMethod, ex.Message);
            return null;
        }
    }

    public static async Task<bool> ShowMenuPanel(this IJSRuntime js,
        string htmlId, string className, string cssDisplay, object thisComponent, string methodName = "")
    {
        string jsMethod = "showMenuPanel";
        try
        {
            await js.InvokeVoidAsync(jsMethod, htmlId, className, cssDisplay);
            return true;
        }
        catch (Exception ex)
        {
            CatchException(thisComponent, methodName, jsMethod, ex.Message);
            return false;
        }
    }

    public static async Task<JsLocation?> GetLocation(this IJSRuntime js, object thisComponent, string methodName = "")
    {
        string jsMethod = "getLocation";
        try
        {
            var result = new JsLocation
            {
                Hash = await js.InvokeAsync<string>(jsMethod, "hash"),
                Host = await js.InvokeAsync<string>(jsMethod, "host"),
                Hostname = await js.InvokeAsync<string>(jsMethod, "hostname"),
                Href = await js.InvokeAsync<string>(jsMethod, "href"),
                Origin = await js.InvokeAsync<string>(jsMethod, "origin"),
                Pathname = await js.InvokeAsync<string>(jsMethod, "pathname"),
                Port = await js.InvokeAsync<string>(jsMethod, "port"),
                Protocol = await js.InvokeAsync<string>(jsMethod, "protocol"),
                Search = await js.InvokeAsync<string>(jsMethod, "search")
            };  
            return result;
        }
        catch (Exception ex)
        {
            CatchException(thisComponent, methodName, jsMethod, ex.Message);
            return null;
        }
    }

    #endregion

    #region Radzen


    public static async Task<bool> ChangeSidebarToggle(this IJSRuntime js,
        string htmlId, int size, object thisComponent, string methodName = "")
    {
        string jsMethod = "changeSidebarToggleStyle";
        try
        {
            await js.InvokeVoidAsync(jsMethod, htmlId, size.ToString() + "px");
            return true;
        }
        catch (Exception ex)
        {
            CatchException(thisComponent, methodName, jsMethod, ex.Message);
            return false;
        }
    }


    #endregion

}