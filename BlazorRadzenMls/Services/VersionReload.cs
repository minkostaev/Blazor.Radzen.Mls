namespace BlazorRadzenMls.Services;

using BlazorRadzenMls.Models;
using Microsoft.JSInterop;
using System.Timers;

public class VersionReload
{
    public Task Initialize { get; }// !!! await !!!
    public bool Initiated { get; protected set; }// optional

    public VersionReload(IJSRuntime js, string afterDomain = "")
    {
        SetTimer(js);
        Initialize = GetVersion(js);
        ///Task.FromResult(GetVersion(js)); // doesn't work good enough
        Initiated = true;
    }
    public VersionReload(HttpClient http, string afterDomain = "")
    {
        SetTimer(http);
        Initialize = GetVersion(http);
        Initiated = true;
    }

    // private

    private void SetTimer(object injection)
    {
        _timer = new Timer(10000) { Enabled = true };//10 sec
        _timer.Elapsed += async delegate
        {
            if (injection is HttpClient http)
                await GetVersion(http);
            else if (injection is IJSRuntime js)
                await GetVersion(js);
            TimerTick?.Invoke(_timer, EventArgs.Empty);
        };
    }
    
    private async Task GetVersion(HttpClient http, string afterDomain = "")
    {
        try { Version = await http.GetStringAsync(afterDomain + _versionFile); Version = Version.Trim(); }
        catch { Version = "0.0.0.0"; Console.WriteLine("Error with HttpClient"); }
    }
    private async Task GetVersion(IJSRuntime js, string afterDomain = "")
    {
        try { Version = await js.InvokeAsync<string>("getVersion", afterDomain + _versionFile); Version = Version.Trim(); }
        catch { Version = "0.0.0.0"; Console.WriteLine("Error with IJSRuntime"); }
    }

    private const string _versionFile = "/data/version.txt";
    private Timer? _timer;

    // public

    public event EventHandler? TimerTick;
    public string Version { get; set; } = "";
    public bool NeedUpdate => !AppValues.VersionClient.StartsWith(Version);

    /// <summary>
    /// Calls js method that hard reload the site
    /// </summary>
    /// <param name="js">JS Runtime injection</param>
    /// <returns></returns>
    public static async Task Reload(IJSRuntime js)
    {
        try { await js.InvokeVoidAsync("reload"); }
        catch { Console.WriteLine("js reload error"); }
    }

}