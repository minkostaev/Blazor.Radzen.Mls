namespace BlazorRadzenMls.Services;

using BlazorRadzenMls.Contracts;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

public class JavaScriptCustom(IJSRuntime jsRuntime, IWebAssemblyHostEnvironment environment) : IJavaScriptCustom
{
    private readonly IJSRuntime __jsr = jsRuntime;
    private readonly IWebAssemblyHostEnvironment __env = environment;

    private string? RazorComponent { get; set; }
    private string? CSharpMethod { get; set; }

    public bool IsDevelopment => __env.IsDevelopment();

    public async Task<bool> InvokeVoidAsync
        (string identifier, params object?[]? args)
    {
        bool isSuccess;
        string? razorComponent = RazorComponent;
        string? cSharpMethod = CSharpMethod;
        try
        {
            await __jsr.InvokeVoidAsync(identifier, args);
            isSuccess = true;
        }
        catch (Exception ex)
        {
            CatchException(razorComponent, cSharpMethod, identifier, ex.Message);
            isSuccess = false;
        }
        return isSuccess;
    }

    public async Task<(bool, object?)> InvokeAsync<[
        DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors |
        DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)
        ] TValue>(string identifier, params object?[]? args)
    {
        bool isSuccess;
        object? result = null;
        string? razorComponent = RazorComponent;
        string? cSharpMethod = CSharpMethod;
        try
        {
            result = await __jsr.InvokeAsync<TValue>(identifier, args);
            isSuccess = true;
        }
        catch (Exception ex)
        {
            CatchException(razorComponent, cSharpMethod, identifier, ex.Message);
            isSuccess = false;
        }
        return (isSuccess, result);
    }

    public void DefineComponent(object? component, string? cMethod)
    {
        RazorComponent = component?.GetType().Name;
        CSharpMethod = cMethod;
    }
    
    private void CatchException(string? component, string? cMethod, string jsMethod, string exMessage)
    {
        Console.WriteLine($"JS error: {component}.razor -> {cMethod} | {jsMethod}");
        if (IsDevelopment)
            Console.WriteLine(exMessage);
    }

}