namespace BlazorRadzenMls.Extensions;

using Microsoft.JSInterop;

public static class JavaScriptRuntime
{
    private static string Error => "JS error: ";
    private static string ComponentName(object component) => component.GetType().Name;
    private static void CatchException(object thisComponent, string cMethod, string jsMethod, string exMessage)
    {
        string name = ComponentName(thisComponent);
        Console.WriteLine($"{Error}{name}.razor -> {cMethod} | {jsMethod}");
        //Console.WriteLine(exMessage);
    }

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

}