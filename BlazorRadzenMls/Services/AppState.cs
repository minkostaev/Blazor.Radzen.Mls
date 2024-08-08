namespace BlazorRadzenMls.Services;

using Blazored.LocalStorage;
using BlazorRadzenMls.Models;
using System;

public class AppState
{
    private readonly ILocalStorageService _localStorage;
    public AppState(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
        SiteOptions = new AppOptions();
    }

    public AppOptions SiteOptions { get; set; }
    public async Task SaveAppOptions()
    {
        await _localStorage.SetItemAsync(AppOptions.Name, SiteOptions);
    }
    public async Task LoadAppOptions()
    {
        var props = await _localStorage.GetItemAsync<AppOptions>(AppOptions.Name);
        SiteOptions = props ?? new AppOptions();
    }

    public event EventHandler? RefreshEvent;
    public void RefreshPage(string e)
    {
        RefreshEvent?.Invoke(e, EventArgs.Empty); Console.WriteLine(e);
    }

}