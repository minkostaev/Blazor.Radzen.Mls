namespace BlazorRadzenMls.Services;

using BlazorRadzenMls.Models;
using Microsoft.JSInterop;
using System.Timers;

public class VersionChecking
{
    public Task Initialize { get; }// !!! await !!!
    public bool Initiated { get; protected set; }// optional

    public VersionChecking(IJSRuntime js)
    {
        SetTimer(js);
        Initialize = GetVersion(js);
        //Task.FromResult(GetVersion(js)); // doesn't work well enough
        Initiated = true;
    }
    public VersionChecking(HttpClient http)
    {
        SetTimer(http);
        Initialize = GetVersion(http);
        Initiated = true;
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
        try { Version = await http.GetStringAsync(_versionFile); }
        catch { Version = "0.0.0.0"; Console.WriteLine("Error with HttpClient"); }
    }
    private async Task GetVersion(IJSRuntime js)
    {
        try { Version = await js.InvokeAsync<string>("getVersion"); }
        catch { Version = "0.0.0.0"; Console.WriteLine("Error with IJSRuntime"); }
    }

    private const string _versionFile = "data/version.txt";
    private Timer? _timer;

    // public

    public event EventHandler? TimerTick;
    public string Version { get; set; } = "";
    //public bool NeedUpdate => !AppValues.Version.StartsWith(Version);
    public bool NeedUpdate
    {
        get
        {
            if (!string.IsNullOrEmpty(AppValues.Version))
                return !AppValues.Version.StartsWith(Version);
            else
                return true;
        }
    }

}