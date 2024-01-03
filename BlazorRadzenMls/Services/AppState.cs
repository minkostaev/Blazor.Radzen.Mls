namespace BlazorRadzenMls.Services;

using System;

public class AppState
{
    public event EventHandler? RefreshEvent;
    public void RefreshPage(string e) { RefreshEvent?.Invoke(e, EventArgs.Empty); }

    //public string Language { get; set; } = "en";

}