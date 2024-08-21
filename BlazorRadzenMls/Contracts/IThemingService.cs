namespace BlazorRadzenMls.Contracts;

public interface IThemingService
{
    string[] ThemesAll { get; }
    string[] ThemesBasic { get; }
    bool? IsDark { get; set; }
    Task<string?> GetTheme();
    Task<bool> SetTheme(string? name, bool saveLocal = false);
    Task UseIsDarkLightProperty();
    string CheckForDarkTheme(string themeName);
}