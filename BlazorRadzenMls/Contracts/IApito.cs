namespace BlazorRadzenMls.Contracts;

using BlazorRadzenMls.Models;
using BlazorRadzenMls.Services;
using Forms.Wpf.Mls.Tools.Models.TheMachine;
using System.Net;

public interface IApito
{
    long RequestTime { get; set; }
    long DeserializeTime { get; set; }
    HttpStatusCode ResponseStatus { get; set; }
    Task<WeatherForecast[]?> GetSomethingAsync();
    Task<MachinesLogs[]?>? GetMachinesLogs();
    Task<bool> DeleteMachinesLogs(string[]? ids);
    Task<MachineDb[]?> GetMachinesDetails();
}