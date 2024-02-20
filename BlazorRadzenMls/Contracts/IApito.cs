namespace BlazorRadzenMls.Contracts;

using BlazorRadzenMls.Services;

public interface IApito
{
    Task<WeatherForecast[]?> GetSomethingAsync();
}