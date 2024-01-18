namespace BlazorRadzenMls.Services;

using Radzen;
using System;

public class AppState
{
    public AppState()
    {
        VersionServer = "0.0.0.0";
    }

    public string VersionServer { get; set; }

    public event EventHandler? RefreshEvent;
    public void RefreshPage(string e) { RefreshEvent?.Invoke(e, EventArgs.Empty); }

    public string? Language { get; set; }

    public string? Theme { get; set; }

    public MenuItemDisplayStyle MenuStyle { get; set; } = MenuItemDisplayStyle.IconAndText;
    //public bool MenuArrow { get; set; } = true;
    //public bool MenuMultiple { get; set; }

}