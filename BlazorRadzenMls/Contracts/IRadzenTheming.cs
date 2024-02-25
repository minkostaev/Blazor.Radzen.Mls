namespace BlazorRadzenMls.Contracts;

public interface IRadzenTheming
{
    string[] Themes { get; set; }
    Task<string> GetTheme();
    Task<bool> SetTheme(string? name, bool saveLocal = false);
}