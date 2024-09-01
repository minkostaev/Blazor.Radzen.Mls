namespace BlazorRadzenMls.Extensions;

using BlazorRadzenMls.Contracts;
using BlazorRadzenMls.Models;
using BlazorRadzenMls.Services;
using Microsoft.AspNetCore.Components;

public static class JavaScriptExtensions
{
    #region Uses default js methods

    public static async Task<bool> CopyToClipboard(this IJavaScriptService js,
        string copy, object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        return await js.InvokeVoidAsync("navigator.clipboard.writeText", copy);
    }

    public static async Task<bool> SessionClear(this IJavaScriptService js,
        object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        return await js.InvokeVoidAsync("sessionStorage.clear");
    }
    
    public static async Task<bool> OpenNewTab(this IJavaScriptService js,
        string url, object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        return await js.InvokeVoidAsync("open", url, "_blank");
    }

    public static async Task<bool> ScrollToTop(this IJavaScriptService js,
        object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        return await js.InvokeVoidAsync("scrollToTop");// to do better
    }

    #endregion

    #region Uses custom coded methods in a external js file

    public static async Task<bool> AddHeaderHeight(this IJavaScriptService js,
        string htmlId, object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        return await js.InvokeVoidAsync("addHeaderFooterHeight", htmlId, true);
    }
    public static async Task<bool> AddFooterHeight(this IJavaScriptService js,
        string htmlId, object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        return await js.InvokeVoidAsync("addHeaderFooterHeight", htmlId, false);
    }

    public static async Task<string?> GetIp(this IJavaScriptService js,
        object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        var result = await js.InvokeAsync<string>("getIp");
        if (result.Item1 && result.Item2 is string ip)
            return ip;
        return null;
    }

    public static async Task<bool> Reload(this IJavaScriptService js,
        object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        return await js.InvokeVoidAsync("reload");
    }

    public static async Task<string> CheckVersion(this IJavaScriptService js,
        NavigationManager _navigationManager, object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        string param = AppStatic.GetGitHubSub(_navigationManager) + "/data/version.txt";
        var result = await js.InvokeAsync<string>("fetchText", param);
        if (result.Item1 && result.Item2 is string version)
            return version.Trim();
        return "0.0.0.0";
    }

    public static async Task<bool?> IsMobileDevice(this IJavaScriptService js,
        object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        var result = await js.InvokeAsync<bool>("isMobileDevice");
        if (result.Item1 && result.Item2 is bool isMobileDevice)
            return isMobileDevice;
        return null;
    }

    public static async Task<bool?> ShowMenuPanel(this IJavaScriptService js,
        string htmlId, string className, string cssDisplay, object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        var result = await js.InvokeAsync<bool?>("showMenuPanel", htmlId, className, cssDisplay);
        if (result.Item1 && result.Item2 is bool isShow)
            return isShow;
        return null;
    }

    public static async Task<JsLocation?> GetLocation(this IJavaScriptService js,
        object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        var result = await js.InvokeAsync<Dictionary<string, string>>("getLocation");
        if (result.Item1 && result.Item2 is Dictionary<string, string> jsProps)
        {
            try
            {
                var location = new JsLocation
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
                return location;
            }
            catch { return null; }
        }
        return null;
    }

    public static async Task<bool> PdfToIframe(this IJavaScriptService js,
        string base64Pdf, string iframeId, object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        return await js.InvokeVoidAsync("pdfToIframe", base64Pdf, iframeId);
    }


    #endregion

    public static async Task<bool?> IsOsDarkTheme(this IJavaScriptService js,
        object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        var result = await js.InvokeAsync<bool?>("isOsDarkTheme");
        if (result.Item1 && result.Item2 is bool theme)
            return theme;
        return null;
    }

    #region Radzen

    public static async Task<string> GetTheme(this IJavaScriptService js,
        object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        var result = await js.InvokeAsync<string>("getRadzenTheme");
        if (result.Item1 && result.Item2 is string theme)
            return theme;
        return string.Empty;
    }

    public static async Task<bool> SetTheme(this IJavaScriptService js,
        string? name, object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        return await js.InvokeVoidAsync("setRadzenTheme", name);
    }

    public static async Task<bool> ChangeSidebarToggle(this IJavaScriptService js,
        string htmlId, int size, object thisComponent, string methodName = "")
    {
        js.DefineComponent(thisComponent, methodName);
        return await js.InvokeVoidAsync("changeSidebarToggleStyle", htmlId, size.ToString() + "px");
    }


    #endregion

}