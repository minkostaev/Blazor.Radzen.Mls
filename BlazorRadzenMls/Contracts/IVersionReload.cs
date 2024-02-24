namespace BlazorRadzenMls.Contracts;

public interface IVersionReload
{
    Task<bool> CheckVersion();
    Task<bool> Reload();
}