namespace BlazorRadzenMls.Contracts;

public interface IVersionReload
{
    string VersionProject { get; }
    string VersionWwwroot { get; }
    bool NeedUpdate { get; }
    double CheckInterval { get; set; }
    Task CheckVersion();
    Task<bool> Reload();
    event EventHandler? DifferenceEvent;
    event EventHandler? TimerEvent;
}