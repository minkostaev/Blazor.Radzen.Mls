namespace BlazorRadzenMls.Services;

using Blazored.LocalStorage;
using BlazorRadzenMls.Models;
using System;
using System.Timers;

public class AppState
{
    public AppState()
    {
        Timer = new Timer(10000) { Enabled = true };//10 sec
        VersionServer = "0.0.0.0";
        SiteOptions = new AppOptions();
    }

    public Timer Timer { get; set; }
    public string VersionServer { get; set; }
    public bool NeedUpdate { get; set; }

    public AppOptions SiteOptions { get; set; }
    public async Task SaveAppOptions(ILocalStorageService local)
    { await local.SetItemAsync(AppOptions.Name, SiteOptions); }
    public async Task LoadAppOptions(ILocalStorageService local)
    {
        var props = await local.GetItemAsync<AppOptions>(AppOptions.Name);
        SiteOptions = props ?? new AppOptions();
    }

    public event EventHandler? RefreshEvent;
    public void RefreshPage(string e) { RefreshEvent?.Invoke(e, EventArgs.Empty); }

    //public string ReplaceFirst(string text, string oldValue, string newValue)
    //{
    //    int position = text.IndexOf(oldValue);
    //    if (position < 0) { return text; }
    //    text = text.Substring(0, position) + newValue + text.Substring(position + oldValue.Length);
    //    return text;
    //}

}