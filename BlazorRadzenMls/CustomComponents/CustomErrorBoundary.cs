namespace BlazorRadzenMls.CustomComponents;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

public class CustomErrorBoundary : ErrorBoundary
{
    [Inject]
    private IWebAssemblyHostEnvironment? Env { get; set; }

    protected override Task OnErrorAsync(Exception exception)
    {
        if (Env == null || Env.IsDevelopment())
        {
            return base.OnErrorAsync(exception);
        }
        return Task.CompletedTask;
    }

}