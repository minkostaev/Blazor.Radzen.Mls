namespace BlazorRadzenMls.Contracts;

using BlazorRadzenMls.Services;

public interface IThemingService
{
    string[] ThemesAll { get; }
    string[] ThemesBasic { get; }
    bool? IsDark { get; set; }
    Task<string> GetTheme();
    Task<string> SetTheme(string? name);
    bool ThemeExist(string name);
    Task UseIsDarkLightProperty(string? name = "");
    string CheckForDarkTheme(string themeName);
    string? ThemeNameAlwaysLight(string? themeName);
}