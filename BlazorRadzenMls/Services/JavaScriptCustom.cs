namespace BlazorRadzenMls.Services;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

public class JavaScriptCustom(IJSRuntime jsRuntime, IWebAssemblyHostEnvironment environment)/// : IJSRuntime
{
    private readonly IJSRuntime __js = jsRuntime;
    private readonly IWebAssemblyHostEnvironment __env = environment;

    ///public ValueTask<TValue> InvokeAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)] TValue>
    ///    (string identifier, CancellationToken cancellationToken, object?[]? args)
    ///{
    ///    return __js.InvokeAsync<TValue>(identifier, args);
    ///}
    ///public ValueTask<TValue> InvokeAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)] TValue>
    ///    (string identifier, object?[]? args)
    ///{
    ///    return __js.InvokeAsync<TValue>(identifier, args);
    ///}

    public bool IsDevelopment => __env.IsDevelopment();



    ///public object? FromComponent { get; set; }
    ///public string? FromMethod { get; set; }

    private string? razorComponent { get; set; }
    private string? cSharpMethod { get; set; }

    public async Task<bool> InvokeVoidAsync(string identifier, object?[]? args = null)
    {
        bool isSuccess;
        try
        {
            await __js.InvokeVoidAsync(identifier, args);
            isSuccess = true;
        }
        catch (Exception ex)
        {
            CatchException(identifier, ex.Message);
            isSuccess = false;
        }
        return isSuccess;
    }

    public async Task<(bool, object?)> InvokeAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)] TValue>
        (string identifier, object?[]? args = null)
    {
        bool isSuccess;
        object? result = null;
        try
        {
            result = await __js.InvokeAsync<TValue>(identifier, args);
            isSuccess = true;
        }
        catch (Exception ex)
        {
            CatchException(identifier, ex.Message);
            isSuccess = false;
        }
        return (isSuccess, result);
    }

    public void DefineComponent(object? component, string? cMethod)
    {
        razorComponent = component?.GetType().Name;
        cSharpMethod = cMethod;
    }
    private void CatchException(string jsMethod, string exMessage)
    {
        Console.WriteLine($"JS error: {razorComponent}.razor -> {cSharpMethod} | {jsMethod}");
        if (IsDevelopment)
            Console.WriteLine(exMessage);
    }


}