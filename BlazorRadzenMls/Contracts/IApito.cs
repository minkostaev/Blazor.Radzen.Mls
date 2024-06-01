namespace BlazorRadzenMls.Contracts;

using BlazorRadzenMls.Models;
using BlazorRadzenMls.Services;

public interface IApito
{
    Task<WeatherForecast[]?> GetSomethingAsync();
    Task<Response> GetMachinesLogs();
    Task<Response> DeleteMachinesLogs(string[]? ids);
    Task<Response> GetMachinesDetails();
}