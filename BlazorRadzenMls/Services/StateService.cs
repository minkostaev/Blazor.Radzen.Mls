namespace BlazorRadzenMls.Services;

using Blazored.LocalStorage;
using BlazorRadzenMls.Layout;
using BlazorRadzenMls.Models;
using System;

public class StateService
{
    private readonly ILocalStorageService _localStorage;
    public StateService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
        SiteOptions = new AppOptions();
        CurrentLayoutType = (typeof(StickyLayout));
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

    public Type? CurrentLayoutType { get; set; }
    
}