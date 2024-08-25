namespace BlazorRadzenMls.Services;

using Blazored.LocalStorage;
using BlazorRadzenMls.Layout;
using BlazorRadzenMls.Models;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;

public class StateService
{
    private readonly ILocalStorageService _localStorage;
    private readonly IWebAssemblyHostEnvironment _env;
    public StateService(ILocalStorageService localStorage, IWebAssemblyHostEnvironment env)
    {
        _localStorage = localStorage;
        _env = env;
        SiteOptions = new AppOptions();
        CurrentLayoutType = typeof(StickyLayout);
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
    public void RefreshPage(object? component, string? cMethod)
    {
        string? razorComponent = component?.GetType().Name;
        string? cSharpMethod = cMethod;
        var e = $"Refresh event: {razorComponent}.razor -> {cSharpMethod}";
        RefreshEvent?.Invoke(e, EventArgs.Empty);
        if (_env.IsDevelopment())
            Console.WriteLine(e);
    }

    public Type? CurrentLayoutType { get; set; }
    
}