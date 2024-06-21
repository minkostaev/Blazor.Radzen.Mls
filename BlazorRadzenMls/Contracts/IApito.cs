namespace BlazorRadzenMls.Contracts;

using BlazorRadzenMls.Models;

public interface IApito
{
    Task<Response> GetImoti();
    Task<Response> PostImoti(ImotMongo item, bool put = false);
    Task<Response> DeleteImot(string? id);
    Task<Response> GetMachinesRecords();
    Task<Response> GetMachinesLogs();
    Task<Response> DeleteMachinesLogs(string[]? ids);
    Task<Response> GetMachinesDetails();
}