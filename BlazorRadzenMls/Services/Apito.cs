namespace BlazorRadzenMls.Services;

using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
using System.Text;
using BlazorRadzenMls.Contracts;
using BlazorRadzenMls.Models;
using System.Net.Http;
using Forms.Wpf.Mls.Tools.Models.TheMachine;
using System.Diagnostics;

public class Apito : IApito
{
    public HttpStatusCode ResponseStatus { get; set; } = HttpStatusCode.OK;
    public long RequestTime { get; set; }
    public long DeserializeTime { get; set; }

    private const string EndpointTest = "/weatherforecast";
    private const string EndpointMachinesDetails = "/machinesdetails";
    private const string EndpointMachinesLogs = "/machineslogs";

    private readonly HttpClient _httpClient;
    public Apito(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApitoSomee");
        stopwatch = new Stopwatch();
    }

    public async Task<WeatherForecast[]?> GetSomethingAsync()
    {
        //var fileContent = new StreamContent(new MemoryStream(xbrlInstance));

        //var response = await _httpClient.PostAsync(EndpointTest, fileContent);

        //var response = await _httpClient.GetFromJsonAsync<WeatherForecast[]>(EndpointTest);
        var response = await _httpClient.GetAsync(EndpointTest);

        if (response == null)
            return null;
        else
            ResponseStatus = response.StatusCode;

        if (response.StatusCode != HttpStatusCode.OK)
            return null;

        response.EnsureSuccessStatusCode();

        //string error = await response.Content.ReadAsStringAsync();

        return await response.Content.ReadFromJsonAsync<WeatherForecast[]>();
    }

    public async Task<MachinesLogs[]?>? GetMachinesLogs()
    {
        TimerStart();

        var response = await _httpClient.GetAsync(EndpointMachinesLogs);

        if (response == null)
        {
            ResponseStatus = HttpStatusCode.InternalServerError;
            return null;
        }
        else
            ResponseStatus = response.StatusCode;

        if (response.StatusCode != HttpStatusCode.OK)
            return null;

        response.EnsureSuccessStatusCode();

        RequestTime = TimerStop();
        TimerStart();

        MachinesLogs[]? result = null;
        try { result = await response.Content.ReadFromJsonAsync<MachinesLogs[]>(); }
        catch (Exception) { }

        DeserializeTime = TimerStop();

        return result;
    }
    public async Task<bool> DeleteMachinesLogs(string[]? ids)
    {
        if (ids == null)
            return false;

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
            ResponseStatus = response.StatusCode;
            return false;
        }
        response.EnsureSuccessStatusCode();

        //string error = await response.Content.ReadAsStringAsync();

        return true;
    }

    public async Task<MachineDb[]?> GetMachinesDetails()
    {
        TimerStart();

        var response = await _httpClient.GetAsync(EndpointMachinesDetails);

        if (response == null)
        {
            ResponseStatus = HttpStatusCode.InternalServerError;
            return null;
        }
        else
            ResponseStatus = response.StatusCode;

        if (response.StatusCode != HttpStatusCode.OK)
            return null;
        response.EnsureSuccessStatusCode();

        RequestTime = TimerStop();
        TimerStart();

        MachineDb[]? result = null;
        try { result = await response.Content.ReadFromJsonAsync<MachineDb[]>(); }
        catch (Exception) { }

        DeserializeTime = TimerStop();

        return result;
    }

    private Stopwatch stopwatch;
    private void TimerStart()
    {
        stopwatch.Restart();
        stopwatch.Start();
    }
    private long TimerStop()
    {
        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

}
public class WeatherForecast
{
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public string? Summary { get; set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}