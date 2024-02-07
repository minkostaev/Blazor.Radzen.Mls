namespace BlazorRadzenMls.Services;

using Blazored.LocalStorage;
using BlazorRadzenMls.Models;
using System;

public class AppState
{
    public AppState()
    {
        VersionServer = "0.0.0.0";
        SiteOptions = new AppOptions();
    }

    public string VersionServer { get; set; }

    public event EventHandler? RefreshEvent;
    public void RefreshPage(string e) { RefreshEvent?.Invoke(e, EventArgs.Empty); }

    public AppOptions SiteOptions { get; set; }
    public async Task SaveAppOptions(ILocalStorageService local)
    { await local.SetItemAsync(AppOptions.Name, SiteOptions); }
    public async Task LoadAppOptions(ILocalStorageService local)
    {
        var props = await local.GetItemAsync<AppOptions>(AppOptions.Name);
        SiteOptions = props ?? new AppOptions();
    }

    //public string ReplaceFirst(string text, string oldValue, string newValue)
    //{
    //    int position = text.IndexOf(oldValue);
    //    if (position < 0) { return text; }
    //    text = text.Substring(0, position) + newValue + text.Substring(position + oldValue.Length);
    //    return text;
    //}

}