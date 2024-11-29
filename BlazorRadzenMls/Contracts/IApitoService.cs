namespace BlazorRadzenMls.Contracts;

using BlazorRadzenMls.Models;

public interface IApitoService
{
    Task<Response> GetImoti();
    Task<Response> PostImoti(ImotMongo item, bool put = false);
    Task<Response> DeleteImot(string? id);
    Task<Response> GetMachinesRecords();
    Task<Response> GetMachinesLogs();
    Task<Response> DeleteMachinesLogs(string[]? ids);
    Task<Response> GetMachinesDetails();
    Task<Response> PostEmail(string from, string name, string topic, string message);
}