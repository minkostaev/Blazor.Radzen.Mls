namespace BlazorRadzenMls.Contracts;

public interface IVersioningService
{
    string VersionProject { get; }
    string VersionWwwroot { get; }
    bool AutoUpdate { get; set; }
    bool NeedUpdate { get; }
    Task CheckVersion();
    Task<bool> Reload();
    event EventHandler? DifferenceEvent;
    event EventHandler? TimerEvent;
}