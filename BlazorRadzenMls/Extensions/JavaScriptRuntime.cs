namespace BlazorRadzenMls.Extensions;

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
            CatchException(thisComponent, methodName, "CopyToClipboard", ex.Message);
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
            CatchException(thisComponent, methodName, "SessionClear", ex.Message);
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
            CatchException(thisComponent, methodName, "OpenNewTab", ex.Message);
            return false;
        }
    }

    #endregion

    #region Uses custom coded methods in a external js file

    public static async Task<bool> ShowMenuPanel(this IJSRuntime js,
        string htmlId, string className, string cssDisplay, object thisComponent, string methodName = "")
    {
        try
        {
            await js.InvokeVoidAsync("showMenuPanel", htmlId, className, cssDisplay);
            return true;
        }
        catch (Exception ex)
        {
            CatchException(thisComponent, methodName, "OpenNewTab", ex.Message);
            return false;
        }
    }

    #endregion

}