namespace BlazorRadzenMls.Services;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

public class JavaScriptCustom(IJSRuntime jsRuntime, IWebAssemblyHostEnvironment environment) : IJSRuntime
{
    private readonly IJSRuntime __js = jsRuntime;
    private readonly IWebAssemblyHostEnvironment __env = environment;

    public ValueTask<TValue> InvokeAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)] TValue>
        (string identifier, CancellationToken cancellationToken, object?[]? args)
    {
        return __js.InvokeAsync<TValue>(identifier, args);
    }
    public ValueTask<TValue> InvokeAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)] TValue>
        (string identifier, object?[]? args)
    {
        return __js.InvokeAsync<TValue>(identifier, args);
    }

    public bool IsDevelopment => __env.IsDevelopment();

}