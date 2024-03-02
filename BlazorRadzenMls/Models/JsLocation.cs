namespace BlazorRadzenMls.Models;

using Microsoft.JSInterop;

public class JsLocation
{
    public Task? Initialize { get; }// !!! await !!!
    public bool Initiated { get; protected set; }// optional
    public JsLocation(IJSRuntime js)
    {
        try
        {
            Initialize = GetVersion(js);
            Initiated = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(AppValues.JsErrorString("getLocation", "JsLocation"));
            Console.WriteLine(ex.Message);
        }
    }
    private async Task GetVersion(IJSRuntime js)
    {
        try
        {
            Hash = await js.InvokeAsync<string>("getLocation", "hash");
            Host = await js.InvokeAsync<string>("getLocation", "host");
            Hostname = await js.InvokeAsync<string>("getLocation", "hostname");
            Href = await js.InvokeAsync<string>("getLocation", "href");
            Origin = await js.InvokeAsync<string>("getLocation", "origin");
            Pathname = await js.InvokeAsync<string>("getLocation", "pathname");
            Port = await js.InvokeAsync<string>("getLocation", "port");
            Protocol = await js.InvokeAsync<string>("getLocation", "protocol");
            Search = await js.InvokeAsync<string>("getLocation", "search");
        }
        catch (Exception ex)
        {
            Console.WriteLine(AppValues.JsErrorString("getLocation", "AppInformation"));
            Console.WriteLine(ex.Message);
        }
    }
    public string? Hash { get; set; }
    public string? Host { get; set; }
    public string? Hostname { get; set; }
    public string? Href { get; set; }
    public string? Origin { get; set; }
    public string? Pathname { get; set; }
    public string? Port { get; set; }
    public string? Protocol { get; set; }
    public string? Search { get; set; }
}
//function getLocation(prop)
//{
//    switch (prop)
//    {
//        case 'hash':
//            return location.hash;
//        case 'host':
//            return location.host;
//        case 'hostname':
//            return location.hostname;
//        case 'href':
//            return location.href;
//        case 'origin':
//            return location.origin;
//        case 'pathname':
//            return location.pathname;
//        case 'port':
//            return location.port;
//        case 'protocol':
//            return location.protocol;
//        case 'search':
//            return location.search;
//        default:
//            return location;
//    }
//}