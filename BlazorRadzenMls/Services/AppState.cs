namespace BlazorRadzenMls.Services;

using Radzen;
using System;

public class AppState
{
    public AppState()
    {
        VersionServer = "0.0.0.0";
        MenuStyle = MenuItemDisplayStyle.IconAndText;
    }

    public string VersionServer { get; set; }

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

    //public string ReplaceFirst(string text, string oldValue, string newValue)
    //{
    //    int position = text.IndexOf(oldValue);
    //    if (position < 0) { return text; }
    //    text = text.Substring(0, position) + newValue + text.Substring(position + oldValue.Length);
    //    return text;
    //}

}