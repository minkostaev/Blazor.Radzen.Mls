namespace BlazorRadzenMls.Services;

using Radzen;
using System;

public class AppState
{
    public AppState()
    {
        MenuStyle = MenuItemDisplayStyle.IconAndText;
    }

    public string VersionServer { get; set; } = "0.0.0.0";

    public event EventHandler? RefreshEvent;
    public void RefreshPage(string e) { RefreshEvent?.Invoke(e, EventArgs.Empty); }

    public string? Language { get; set; }

    public string? Theme { get; set; }

    public string? SidebarWidth { get; private set; }
    public MenuItemDisplayStyle _menuStyle;
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
    public bool MenuArrow { get; set; } = true;
    public bool MenuMultiple { get; set; } = true;

}