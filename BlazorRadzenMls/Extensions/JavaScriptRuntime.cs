namespace BlazorRadzenMls.Extensions;

using BlazorRadzenMls.Models;
using BlazorRadzenMls.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Xml.Linq;

public static class JavaScriptRuntime
{

    #region Error showing on crash
    private static string Error => "JS error: ";
    private static string ComponentName(object component) => component.GetType().Name;
    private static void CatchException(object thisComponent, string cMethod, string jsMethod, string exMessage)
    {
        string name = ComponentName(thisComponent);
        Console.WriteLine($"{Error}{name}.razor -> {cMethod} | {jsMethod}");
        Console.WriteLine(exMessage);
    }
    #endregion

    #region Uses default js methods

    public static async Task<bool> ScrollToTop(this JavaScriptCustom js, object thisComponent, string methodName = "")
    {
        string jsMethod = "scrollToTop";
        try
        {
            await js.InvokeVoidAsync(jsMethod);
            return true;
        }
        catch (Exception ex)
        {
            CatchException(thisComponent, methodName, jsMethod, ex.Message);
            return false;
        }
    }

    public static async Task<bool> CopyToClipboard(this JavaScriptCustom js, string copy, object thisComponent, string methodName = "")
    {
        try
        {
            await js.InvokeVoidAsync("navigator.clipboard.writeText", [copy]);
            return true;
        }
        catch (Exception ex)
        {
            CatchException(thisComponent, methodName, "navigator.clipboard.writeText", ex.Message);
            return false;
        }
    }

    public static async Task<bool> SessionClear(this JavaScriptCustom js, object thisComponent, string methodName = "")
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
    
    public static async Task<bool> OpenNewTab(this JavaScriptCustom js, string url, object thisComponent, string methodName = "")
    {
        try
        {
            await js.InvokeVoidAsync("open", [url, "_blank"]);
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

    public static async Task<bool> AddHeaderHeight(this JavaScriptCustom js, string htmlId, object thisComponent, string methodName = "")
    {
        string jsMethod = "addHeaderFooterHeight";
        try
        {
            await js.InvokeVoidAsync(jsMethod, [htmlId, true]);
            return true;
        }
        catch (Exception ex)
        {
            CatchException(thisComponent, methodName, jsMethod, ex.Message);
            return false;
        }
    }
    public static async Task<bool> AddFooterHeight(this JavaScriptCustom js, string htmlId, object thisComponent, string methodName = "")
    {
        string jsMethod = "addHeaderFooterHeight";
        try
        {
            await js.InvokeVoidAsync(jsMethod, [htmlId, false]);
            return true;
        }
        catch (Exception ex)
        {
            CatchException(thisComponent, methodName, jsMethod, ex.Message);
            return false;
        }
    }

    public static async Task<string?> GetIp(this JavaScriptCustom js, object thisComponent, string methodName = "")
    {
        //string jsMethod = "getIp";
        //try
        //{
        //    return await js.InvokeAsync<string>(jsMethod);
        //}
        //catch (Exception ex)
        //{
        //    CatchException(thisComponent, methodName, jsMethod, ex.Message);
        //    return null;
        //}
        js.DefineComponent(thisComponent, methodName);
        var result = await js.InvokeAsync<string>("getIp");
        if (result.Item1 && result.Item2 is string ip)
            return ip;
        return null;
    }

    public static async Task<bool> Reload(this JavaScriptCustom js, object thisComponent, string methodName = "")
    {
        string jsMethod = "reload";
        try
        {
            await js.InvokeVoidAsync(jsMethod);
            return true;
        }
        catch (Exception ex)
        {
            CatchException(thisComponent, methodName, jsMethod, ex.Message);
            return false;
        }
    }

    public static async Task<string> CheckVersion(this JavaScriptCustom js, NavigationManager _navigationManager, object thisComponent, string methodName = "")
    {
        //string jsMethod = "fetchText";
        //try
        //{
        //    string param = AppStatic.GetGitHubSub(_navigationManager) + "/data/version.txt";
        //    string version = await js.InvokeAsync<string>(jsMethod, param);
        //    return version.Trim();
        //}
        //catch (Exception ex)
        //{
        //    CatchException(thisComponent, methodName, jsMethod, ex.Message);
        //    return "0.0.0.0";
        //}
        js.DefineComponent(thisComponent, methodName);
        string param = AppStatic.GetGitHubSub(_navigationManager) + "/data/version.txt";
        var result = await js.InvokeAsync<string>("fetchText", [param]);
        if (result.Item1 && result.Item2 is string version)
            return version.Trim();
        return "0.0.0.0";
    }

    public static async Task<bool?> IsMobileDevice(this JavaScriptCustom js, object thisComponent, string methodName = "")
    {
        //string jsMethod = "isMobileDevice";
        //try
        //{
        //    return await js.InvokeAsync<bool>(jsMethod);
        //}
        //catch (Exception ex)
        //{
        //    CatchException(thisComponent, methodName, jsMethod, ex.Message);
        //    return null;
        //}
        js.DefineComponent(thisComponent, methodName);
        var result = await js.InvokeAsync<bool>("isMobileDevice");
        if (result.Item1 && result.Item2 is bool isMobileDevice)
            return isMobileDevice;
        return null;
    }

    public static async Task<bool?> ShowMenuPanel(this JavaScriptCustom js,
        string htmlId, string className, string cssDisplay, object thisComponent, string methodName = "")
    {
        //string jsMethod = "showMenuPanel";
        //try
        //{
        //    return await js.InvokeAsync<bool?>(jsMethod, htmlId, className, cssDisplay);
        //}
        //catch (Exception ex)
        //{
        //    CatchException(thisComponent, methodName, jsMethod, ex.Message);
        //    return null;
        //}
        js.DefineComponent(thisComponent, methodName);
        var result = await js.InvokeAsync<bool?>("showMenuPanel", [htmlId, className, cssDisplay]);
        if (result.Item1 && result.Item2 is bool isShow)
            return isShow;
        return null;
    }

    public static async Task<JsLocation?> GetLocation(this JavaScriptCustom js, object thisComponent, string methodName = "")
    {
        //string jsMethod = "getLocation";
        //try
        //{
        //    var result = new JsLocation
        //    {
        //        Hash = await js.InvokeAsync<string>(jsMethod, "hash"),
        //        Host = await js.InvokeAsync<string>(jsMethod, "host"),
        //        Hostname = await js.InvokeAsync<string>(jsMethod, "hostname"),
        //        Href = await js.InvokeAsync<string>(jsMethod, "href"),
        //        Origin = await js.InvokeAsync<string>(jsMethod, "origin"),
        //        Pathname = await js.InvokeAsync<string>(jsMethod, "pathname"),
        //        Port = await js.InvokeAsync<string>(jsMethod, "port"),
        //        Protocol = await js.InvokeAsync<string>(jsMethod, "protocol"),
        //        Search = await js.InvokeAsync<string>(jsMethod, "search")
        //    };  
        //    return result;
        //}
        //catch (Exception ex)
        //{
        //    CatchException(thisComponent, methodName, jsMethod, ex.Message);
        //    return null;
        //}
        js.DefineComponent(thisComponent, methodName);

        //List<string> propsValues = new List<string>();
        var jsProps = new Dictionary<string, string?>()
        {
            { "hash", null},
            { "host", null},
            { "hostname", null},
            { "href", null},
            { "origin", null},
            { "pathname", null},
            { "port", null},
            { "protocol", null},
            { "search", null}
        };
        //string[] propsNames = ["hash", "host", "hostname", "href", "origin", "pathname", "port", "protocol", "search"];
        foreach (string key in jsProps.Keys)
        {
            var jsResult = await js.InvokeAsync<string>("getLocation", [key]);
            if (jsResult.Item1 && jsResult.Item2 is string val)
                jsProps[key] = val;
        }

        var result = new JsLocation
        {
            Hash = jsProps["hash"],
            Host = jsProps["host"],
            Hostname = jsProps["hostname"],
            Href = jsProps["href"],
            Origin = jsProps["origin"],
            Pathname = jsProps["pathname"],
            Port = jsProps["port"],
            Protocol = jsProps["protocol"],
            Search = jsProps["search"]
        };

        //var result = await js.InvokeAsync<string>("getLocation");
        //if (result.Item1 && result.Item2 is string ip)
        //    return ip;
        return result;
    }



    public static async Task<bool> PdfToIframe(this JavaScriptCustom js,
        string base64Pdf, string iframeId, object thisComponent, string methodName = "")
    {
        //try { await __js.InvokeVoidAsync("pdfToIframe", base64Pdf, iframeId); }
        //catch { Console.WriteLine("js error: IframeVsObject.razor -> OnAfterRenderAsync"); }

        js.DefineComponent(thisComponent, methodName);
        return await js.InvokeVoidAsync("pdfToIframe", [base64Pdf, iframeId]);
    }



    #endregion

    #region Radzen

    public static async Task<string> GetTheme(this JavaScriptCustom js, object thisComponent, string methodName = "")
    {
        //string jsMethod = "getRadzenTheme";
        //try
        //{
        //    return await js.InvokeAsync<string>(jsMethod);
        //}
        //catch (Exception ex)
        //{
        //    CatchException(thisComponent, methodName, jsMethod, ex.Message);
        //    return string.Empty;
        //}
        js.DefineComponent(thisComponent, methodName);
        var result = await js.InvokeAsync<string>("getRadzenTheme");
        if (result.Item1 && result.Item2 is string theme)
            return theme;
        return string.Empty;
    }

    public static async Task<bool> SetTheme(this JavaScriptCustom js, string? name, object thisComponent, string methodName = "")
    {
        //string jsMethod = "setRadzenTheme";
        //try
        //{
        //    await js.InvokeVoidAsync(jsMethod, name);
        //    return true;
        //}
        //catch (Exception ex)
        //{
        //    CatchException(thisComponent, methodName, jsMethod, ex.Message);
        //    return false;
        //}
        js.DefineComponent(thisComponent, methodName);
        return await js.InvokeVoidAsync("setRadzenTheme", [name]);
    }

    public static async Task<bool> ChangeSidebarToggle(this JavaScriptCustom js,
        string htmlId, int size, object thisComponent, string methodName = "")
    {
        //string jsMethod = "changeSidebarToggleStyle";
        //try
        //{
        //    await js.InvokeVoidAsync(jsMethod, htmlId, size.ToString() + "px");
        //    return true;
        //}
        //catch (Exception ex)
        //{
        //    CatchException(thisComponent, methodName, jsMethod, ex.Message);
        //    return false;
        //}
        js.DefineComponent(thisComponent, methodName);
        return await js.InvokeVoidAsync("changeSidebarToggleStyle", [htmlId, size.ToString() + "px"]);
    }


    #endregion

}