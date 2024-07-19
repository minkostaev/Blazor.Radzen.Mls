namespace BlazorRadzenMls.Contracts;

public interface IVersionReload
{
    string VersionProject { get; }
    string VersionCached { get; }
    bool NeedUpdate { get; }
    Task CheckVersion();
    Task<bool> Reload();
}