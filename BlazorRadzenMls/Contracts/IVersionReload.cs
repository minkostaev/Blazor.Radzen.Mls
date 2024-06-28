namespace BlazorRadzenMls.Contracts;

public interface IVersionReload
{
    Task CheckVersion();
    Task<bool> Reload();
}