namespace BlazorRadzenMls.Contracts;

public interface IVersionReload
{
    string VersionProject { get; }
    string VersionWwwroot { get; }
    bool NeedUpdate { get; }
    Task CheckVersion();
    Task<bool> Reload();
}