namespace BlazorRadzenMls.Services;

using BlazorRadzenMls.Contracts;
using BlazorRadzenMls.Models;
using BlazorRadzenMls.Models.TheMachine;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

public class Apito : IApito
{
    private const string EndpointMachinesDetails = "/machinesdetails";
    private const string EndpointMachinesLogs = "/machineslogs";
    private const string EndpointMachinesRecords = "/machinesrecords";
    private const string EndpointImoti = "/imoti";

    private readonly HttpClient _httpClient;
    public Apito(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApitoSomee");
    }

    public async Task<Response> GetMachinesLogs()
    {
        var (response, result) = await AppStatic.GetResponse(_httpClient, EndpointMachinesLogs);

        var timer2 = AppStatic.TimerStart();

        try { result.Result = await response!.Content.ReadFromJsonAsync<MachinesLogs[]>(); }
        catch (Exception) { }

        result.DeserializeTime = AppStatic.TimerStop(timer2);

        return result;
    }
    public async Task<Response> DeleteMachinesLogs(string[]? ids)
    {
        var result = new Response();
        if (ids == null)
        {
            result.Status = HttpStatusCode.InternalServerError;
            return result;
        }

        var timer = AppStatic.TimerStart();
        string jsonString = JsonSerializer.Serialize(ids);
        string requestBody = "{\"Ids\": " + jsonString + "}";
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri(string.Concat(_httpClient.BaseAddress!.AbsoluteUri, EndpointMachinesLogs.AsSpan(1))),
            Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
        };

        HttpResponseMessage response = await _httpClient.SendAsync(request);

        if (response.StatusCode != HttpStatusCode.NoContent)
        {
            result.Status = response.StatusCode;
            return result;
        }
        response.EnsureSuccessStatusCode();

        result.RequestTime = AppStatic.TimerStop(timer);
        result.Result = true;

        return result;
    }

    public async Task<Response> GetMachinesDetails()
    {
        var (response, result) = await AppStatic.GetResponse(_httpClient, EndpointMachinesDetails);

        var timer = AppStatic.TimerStart();

        try { result.Result = await response!.Content.ReadFromJsonAsync<MachineDb[]>(); }
        catch (Exception) { }

        result.DeserializeTime = AppStatic.TimerStop(timer);

        return result;
    }

    public async Task<Response> GetMachinesRecords()
    {
        var (response, result) = await AppStatic.GetResponse(_httpClient, EndpointMachinesRecords);

        var timer = AppStatic.TimerStart();

        try { result.Result = await response!.Content.ReadFromJsonAsync<MachinesRecords[]>(); }
        catch (Exception) { }

        result.DeserializeTime = AppStatic.TimerStop(timer);

        return result;
    }

    public async Task<Response> GetImoti()
    {
        var (response, result) = await AppStatic.GetResponse(_httpClient, EndpointImoti);

        var timer = AppStatic.TimerStart();

        try { result.Result = await response!.Content.ReadFromJsonAsync<ImotMongo[]>(); }
        catch (Exception) { }

        result.DeserializeTime = AppStatic.TimerStop(timer);

        return result;
    }

    public async Task<Response> PostImoti(ImotMongo item, bool put = false)
    {
        var result = new Response();
        var timer1 = AppStatic.TimerStart();

        HttpResponseMessage response;
        if (!put)
            response = await _httpClient.PostAsJsonAsync(EndpointImoti, item as Imot);
        else
        {
            string? id = item?.Id;
            Imot? imot = item;
            response = await _httpClient.PutAsJsonAsync($"{EndpointImoti}/{id}", imot);
        }

        if (response == null)
        {
            result.Status = HttpStatusCode.InternalServerError;
            return result;
        }
        else
            result.Status = response.StatusCode;

        if (response.StatusCode != HttpStatusCode.OK)
            return result;

        response.EnsureSuccessStatusCode();

        result.RequestTime = AppStatic.TimerStop(timer1);
        var timer2 = AppStatic.TimerStart();

        try { result.Result = await response.Content.ReadFromJsonAsync<Imot[]>(); }
        catch (Exception) { }

        result.DeserializeTime = AppStatic.TimerStop(timer2);

        return result;
    }

    public async Task<Response> DeleteImot(string? id)
    {
        var result = new Response();
        if (id == null)
        {
            result.Status = HttpStatusCode.InternalServerError;
            return result;
        }

        var timer = AppStatic.TimerStart();

        HttpResponseMessage response = await _httpClient.DeleteAsync($"{EndpointImoti}/{id}");

        if (response.StatusCode != HttpStatusCode.NoContent)
        {
            result.Status = response.StatusCode;
            return result;
        }
        response.EnsureSuccessStatusCode();

        result.RequestTime = AppStatic.TimerStop(timer);
        result.Result = true;

        return result;
    }

}