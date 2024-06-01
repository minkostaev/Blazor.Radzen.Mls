namespace BlazorRadzenMls.Services;

using BlazorRadzenMls.Contracts;
using BlazorRadzenMls.Models;
using Forms.Wpf.Mls.Tools.Models.TheMachine;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

public class Apito : IApito
{
    private const string EndpointTest = "/weatherforecast";
    private const string EndpointMachinesDetails = "/machinesdetails";
    private const string EndpointMachinesLogs = "/machineslogs";

    private readonly HttpClient _httpClient;
    public Apito(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApitoSomee");
    }

    public async Task<WeatherForecast[]?> GetSomethingAsync()
    {
        //var fileContent = new StreamContent(new MemoryStream(xbrlInstance));

        //var response = await _httpClient.PostAsync(EndpointTest, fileContent);

        //var response = await _httpClient.GetFromJsonAsync<WeatherForecast[]>(EndpointTest);
        var response = await _httpClient.GetAsync(EndpointTest);

        if (response == null)
            return null;
        //else
        //    ResponseStatus = response.StatusCode;

        if (response.StatusCode != HttpStatusCode.OK)
            return null;

        response.EnsureSuccessStatusCode();

        //string error = await response.Content.ReadAsStringAsync();

        return await response.Content.ReadFromJsonAsync<WeatherForecast[]>();
    }

    public async Task<Response> GetMachinesLogs()
    {
        var result = new Response();
        var timer1 = TimerStart();

        var response = await _httpClient.GetAsync(EndpointMachinesLogs);

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

        result.RequestTime = TimerStop(timer1);
        var timer2 = TimerStart();

        //MachinesLogs[]? result = null;
        try { result.Result = await response.Content.ReadFromJsonAsync<MachinesLogs[]>(); }
        catch (Exception) { }

        result.DeserializeTime = TimerStop(timer2);

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

        var timer = TimerStart();
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

        result.RequestTime = TimerStop(timer);
        result.Result = true;

        return result;
    }
        
    public async Task<Response> GetMachinesDetails()
    {
        var result = new Response();
        var timer1 = TimerStart();
        
        var response = await _httpClient.GetAsync(EndpointMachinesDetails);

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

        result.RequestTime = TimerStop(timer1);
        var timer2 = TimerStart();

        try { result.Result = await response.Content.ReadFromJsonAsync<MachineDb[]>(); }
        catch (Exception) { }

        result.DeserializeTime = TimerStop(timer2);

        return result;
    }

    private Stopwatch TimerStart()
    {
        var stopwatch = new Stopwatch();
        //stopwatch.Restart();
        stopwatch.Start();
        return stopwatch;
    }
    private long TimerStop(Stopwatch stopwatch)
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