namespace BlazorRadzenMls.Contracts;

public interface IThemingService
{
    string[] Themes { get; }
    Task<string?> GetTheme();
    Task<bool> SetTheme(string? name, bool saveLocal = false);
}