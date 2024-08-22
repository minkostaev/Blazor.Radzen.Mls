namespace BlazorRadzenMls.CustomComponents;

using BlazorRadzenMls.Services;
using Microsoft.AspNetCore.Components;

public class BaseComponent : ComponentBase
{
    [Inject]
    private StateService? __state { get; set; }
    protected override void OnInitialized()
    {
        __state!.RefreshEvent += delegate { StateHasChanged(); };///Refresh_Event
    }
    ///private void Refresh_Event(object? sender, EventArgs e) { StateHasChanged(); }
    
    ///public void Dispose()
    ///{
    ///    __state.RefreshEvent -= delegate { StateHasChanged(); };
    ///    base.Dispose();
    ///}
}