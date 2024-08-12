namespace BlazorRadzenMls.Contracts;

using System.Diagnostics.CodeAnalysis;

public interface IJavaScriptService
{
    bool IsDevelopment { get; }
    void DefineComponent(object? component, string? cMethod);
    Task<bool> InvokeVoidAsync
        (string identifier, params object?[]? args);
    Task<(bool, object?)> InvokeAsync<[
        DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors |
        DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)
        ] TValue>(string identifier, params object?[]? args);
}