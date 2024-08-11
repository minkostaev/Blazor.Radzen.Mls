namespace BlazorRadzenMls.Extensions;

using BlazorRadzenMls.Contracts;
using BlazorRadzenMls.Models;
using BlazorRadzenMls.Services;
using Microsoft.AspNetCore.Components;

public static class JsCustom
{
    #region Uses default js methods

    public static async Task<bool> CopyToClipboard(this IJavaScriptCustom js,
        string copy, object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        return await js.InvokeVoidAsync("navigator.clipboard.writeText", copy);
    }

    public static async Task<bool> SessionClear(this IJavaScriptCustom js,
        object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        return await js.InvokeVoidAsync("sessionStorage.clear");
    }
    
    public static async Task<bool> OpenNewTab(this IJavaScriptCustom js,
        string url, object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        return await js.InvokeVoidAsync("open", url, "_blank");
    }

    public static async Task<bool> ScrollToTop(this IJavaScriptCustom js,
        object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        return await js.InvokeVoidAsync("scrollToTop");// to do better
    }

    #endregion

    #region Uses custom coded methods in a external js file

    public static async Task<bool> AddHeaderHeight(this IJavaScriptCustom js,
        string htmlId, object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        return await js.InvokeVoidAsync("addHeaderFooterHeight", htmlId, true);
    }
    public static async Task<bool> AddFooterHeight(this IJavaScriptCustom js,
        string htmlId, object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        return await js.InvokeVoidAsync("addHeaderFooterHeight", htmlId, false);
    }

    public static async Task<string?> GetIp(this IJavaScriptCustom js,
        object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        var result = await js.InvokeAsync<string>("getIp");
        if (result.Item1 && result.Item2 is string ip)
            return ip;
        return null;
    }

    public static async Task<bool> Reload(this IJavaScriptCustom js,
        object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        return await js.InvokeVoidAsync("reload");
    }

    public static async Task<string> CheckVersion(this IJavaScriptCustom js,
        NavigationManager _navigationManager, object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        string param = AppStatic.GetGitHubSub(_navigationManager) + "/data/version.txt";
        var result = await js.InvokeAsync<string>("fetchText", param);
        if (result.Item1 && result.Item2 is string version)
            return version.Trim();
        return "0.0.0.0";
    }

    public static async Task<bool?> IsMobileDevice(this IJavaScriptCustom js,
        object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        var result = await js.InvokeAsync<bool>("isMobileDevice");
        if (result.Item1 && result.Item2 is bool isMobileDevice)
            return isMobileDevice;
        return null;
    }

    public static async Task<bool?> ShowMenuPanel(this IJavaScriptCustom js,
        string htmlId, string className, string cssDisplay, object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        var result = await js.InvokeAsync<bool?>("showMenuPanel", htmlId, className, cssDisplay);
        if (result.Item1 && result.Item2 is bool isShow)
            return isShow;
        return null;
    }

    public static async Task<JsLocation?> GetLocation(this IJavaScriptCustom js,
        object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
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
        bool success = true;
        foreach (string key in jsProps.Keys)
        {
            var jsResult = await js.InvokeAsync<string>("getLocation", key);
            if (jsResult.Item1 && jsResult.Item2 is string val)
                jsProps[key] = val;
            if (!jsResult.Item1)
                success = false;
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
        if (success)
            return result;
        return null;
    }

    public static async Task<bool> PdfToIframe(this IJavaScriptCustom js,
        string base64Pdf, string iframeId, object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        return await js.InvokeVoidAsync("pdfToIframe", base64Pdf, iframeId);
    }


    #endregion

    #region Radzen

    public static async Task<string> GetTheme(this IJavaScriptCustom js,
        object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        var result = await js.InvokeAsync<string>("getRadzenTheme");
        if (result.Item1 && result.Item2 is string theme)
            return theme;
        return string.Empty;
    }

    public static async Task<bool> SetTheme(this IJavaScriptCustom js,
        string? name, object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        return await js.InvokeVoidAsync("setRadzenTheme", name);
    }

    public static async Task<bool> ChangeSidebarToggle(this IJavaScriptCustom js,
        string htmlId, int size, object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        return await js.InvokeVoidAsync("changeSidebarToggleStyle", htmlId, size.ToString() + "px");
    }


    #endregion

}