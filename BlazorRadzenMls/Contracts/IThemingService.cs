namespace BlazorRadzenMls.Contracts;

using BlazorRadzenMls.Services;

public interface IThemingService
{
    string[] ThemesAll { get; }
    string[] ThemesBasic { get; }
    bool? IsDark { get; set; }
    Task<string?> GetTheme(StateService? state = null);
    Task<bool> SetTheme(string? name, StateService? __state = null, bool saveLocal = false);
    Task UseIsDarkLightProperty(StateService? state = null);
    string CheckForDarkTheme(string themeName);
}