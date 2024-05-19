namespace BlazorRadzenMls.Contracts;

using BlazorRadzenMls.Models;
using BlazorRadzenMls.Services;

public interface IApito
{
    Task<WeatherForecast[]?> GetSomethingAsync();
    Task<MachinesLogs[]> GetMachinesLogs();
    Task<bool> DeleteMachinesLogs(string[]? ids);
}