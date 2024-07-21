namespace BlazorRadzenMls.Services;

using Blazored.LocalStorage;
using BlazorRadzenMls.Models;
using System;
using System.Timers;

public class AppState
{
    private readonly ILocalStorageService _localStorage;
    public AppState(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
        //Timer = new Timer(10000) { Enabled = true };//10 sec
        SiteOptions = new AppOptions();
    }

    //public Timer Timer { get; set; }

    public AppOptions SiteOptions { get; set; }
    public async Task SaveAppOptions()
    { await _localStorage.SetItemAsync(AppOptions.Name, SiteOptions); }
    public async Task LoadAppOptions()
    {
        var props = await _localStorage.GetItemAsync<AppOptions>(AppOptions.Name);
        SiteOptions = props ?? new AppOptions();
    }

    public event EventHandler? RefreshEvent;
    public void RefreshPage(string e)
    { RefreshEvent?.Invoke(e, EventArgs.Empty); Console.WriteLine(e); }

    //public string ReplaceFirst(string text, string oldValue, string newValue)
    //{
    //    int position = text.IndexOf(oldValue);
    //    if (position < 0) { return text; }
    //    text = text.Substring(0, position) + newValue + text.Substring(position + oldValue.Length);
    //    return text;
    //}

}