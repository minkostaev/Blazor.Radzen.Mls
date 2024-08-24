namespace BlazorRadzenMls.Contracts;

using BlazorRadzenMls.Services;

public interface IThemingService
{
    string[] ThemesAll { get; }
    string[] ThemesBasic { get; }
    bool? IsDark { get; set; }
    Task<string?> GetTheme(StateService? __state = null);
    Task<bool> SetTheme(string? name, StateService? __state = null, bool saveLocal = false);
    Task UseIsDarkLightProperty(StateService? __state = null);
    string CheckForDarkTheme(string themeName);
}