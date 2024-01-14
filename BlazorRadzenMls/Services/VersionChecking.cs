namespace BlazorRadzenMls.Services;

using BlazorRadzenMls.Models;
using Microsoft.JSInterop;
using System.Timers;

public class VersionChecking
{
    public VersionChecking(IJSRuntime js)
    {
        _ = GetVersion(js);
        SetTimer(js);
    }
    public VersionChecking(HttpClient http)
    {
        _ = GetVersion(http);
        SetTimer(http);
    }

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
    private async Task GetVersion(HttpClient http)
    {
        try { Version = await http.GetStringAsync("data/version.txt"); }
        catch { Version = "0.0.0.0"; Console.WriteLine("Error with HttpClient"); }
    }
    private async Task GetVersion(IJSRuntime js)
    {
        try { Version = await js.InvokeAsync<string>("getVersion"); }
        catch { Version = "0.0.0.0"; Console.WriteLine("Error with IJSRuntime"); }
    }

    private static Timer? _timer;

    // public

    public event EventHandler? TimerTick;
    public string Version { get; set; } = "";
    public bool NeedUpdate => !AppValues.Version.StartsWith(Version);

}