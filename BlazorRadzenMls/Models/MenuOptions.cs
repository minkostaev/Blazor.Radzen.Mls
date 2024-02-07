namespace BlazorRadzenMls.Models;

using Radzen;

public class MenuOptions
{
    public bool Arrow { get; set; }
    public bool Multiple { get; set; }
    private MenuItemDisplayStyle _menuStyle;
    public MenuItemDisplayStyle MenuStyle
    {
        get
        {
            return _menuStyle;
        }
        set
        {
            switch (value)
            {
                case MenuItemDisplayStyle.IconAndText:
                    SidebarWidth = string.Empty;
                    break;
                case MenuItemDisplayStyle.Text:
                    SidebarWidth = "width: 220px;";
                    break;
                case MenuItemDisplayStyle.Icon:
                    SidebarWidth = "width: 80px;";
                    break;
                default:
                    break;
            }
            _menuStyle = value;
        }
    }
    public string? SidebarWidth { get; private set; }
}