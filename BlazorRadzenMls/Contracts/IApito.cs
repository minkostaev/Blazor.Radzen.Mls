namespace BlazorRadzenMls.Contracts;

using BlazorRadzenMls.Models;
using BlazorRadzenMls.Services;
using Forms.Wpf.Mls.Tools.Models.TheMachine;

public interface IApito
{
    Task<WeatherForecast[]?> GetSomethingAsync();
    Task<MachinesLogs[]?>? GetMachinesLogs();
    Task<bool> DeleteMachinesLogs(string[]? ids);
    Task<Machine[]?> GetMachinesDetails();
}