namespace BlazorRadzenMls.Models;

public class AppOptions
{
    public AppOptions()
    {
        Menu = new MenuOptions
        {
            Arrow = true,
            Multiple = true,
            MenuStyle = Radzen.MenuItemDisplayStyle.IconAndText
        };
    }
    public static string Name => typeof(AppOptions).Name;
    public string? Page { get; set; }
    public string? Language { get; set; }
    public string? Theme { get; set; }
    public int ApitoId { get; set; }
    public MenuOptions Menu { get; set; }
}