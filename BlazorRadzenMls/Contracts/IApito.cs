namespace BlazorRadzenMls.Contracts;

using BlazorRadzenMls.Models;

public interface IApito
{
    Task<Response> GetMachinesRecords();
    Task<Response> GetMachinesLogs();
    Task<Response> DeleteMachinesLogs(string[]? ids);
    Task<Response> GetMachinesDetails();
}