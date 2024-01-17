namespace BlazorRadzenMls.Services;

using System;

public class AppState
{
    public event EventHandler? RefreshEvent;
    public void RefreshPage(string e) { RefreshEvent?.Invoke(e, EventArgs.Empty); }

    public string? Language { get; set; }

    public string? Theme { get; set; }

}
// to see
//https://blazor-university.com/

// Built-in Components:
// App
// Router
// DynamicComponent
// ErrorBoundary
// NavMenu
// NavLink

// EditForm
