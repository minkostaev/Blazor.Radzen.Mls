namespace BlazorRadzenMls.Services;

using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
using System.Text;
using BlazorRadzenMls.Contracts;
using BlazorRadzenMls.Models;
using System.Net.Http;
using Forms.Wpf.Mls.Tools.Models.TheMachine;

public class Apito : IApito
{
    public HttpStatusCode LastResponseStatus { get; set; } = HttpStatusCode.OK;

    //public string LastResponseMessage { get; set; }

    private const string EndpointTest = "/weatherforecast";
    private const string EndpointMachinesDetails = "/machinesdetails";
    private const string EndpointMachinesLogs = "/machineslogs";

    private readonly HttpClient _httpClient;
    public Apito(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApitoSomee");
    }

    public virtual async Task<WeatherForecast[]?> GetSomethingAsync()
    {
        //var fileContent = new StreamContent(new MemoryStream(xbrlInstance));

        //var response = await _httpClient.PostAsync(EndpointTest, fileContent);

        //var response = await _httpClient.GetFromJsonAsync<WeatherForecast[]>(EndpointTest);
        var response = await _httpClient.GetAsync(EndpointTest);

        if (response.StatusCode != HttpStatusCode.OK)
        {
            LastResponseStatus = response.StatusCode;
            return null;
        }
        response.EnsureSuccessStatusCode();

        //string error = await response.Content.ReadAsStringAsync();

        return await response.Content.ReadFromJsonAsync<WeatherForecast[]>();
    }

    public virtual async Task<MachinesLogs[]?>? GetMachinesLogs()
    {
        //var fileContent = new StreamContent(new MemoryStream(xbrlInstance));

        //var response = await _httpClient.PostAsync(EndpointTest, fileContent);

        //var response = await _httpClient.GetFromJsonAsync<WeatherForecast[]>(EndpointTest);
        var response = await _httpClient.GetAsync(EndpointMachinesLogs);

        if (response.StatusCode != HttpStatusCode.OK)
        {
            LastResponseStatus = response.StatusCode;
            return null;
        }
        response.EnsureSuccessStatusCode();

        //string error = await response.Content.ReadAsStringAsync();

        return await response.Content.ReadFromJsonAsync<MachinesLogs[]>();
    }
    public virtual async Task<bool> DeleteMachinesLogs(string[]? ids)
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
            LastResponseStatus = response.StatusCode;
            return false;
        }
        response.EnsureSuccessStatusCode();

        //string error = await response.Content.ReadAsStringAsync();

        return true;
    }

    public virtual async Task<Machine[]?> GetMachinesDetails()
    {
        //var fileContent = new StreamContent(new MemoryStream(xbrlInstance));

        //var response = await _httpClient.PostAsync(EndpointTest, fileContent);

        //var response = await _httpClient.GetFromJsonAsync<WeatherForecast[]>(EndpointTest);
        var response = await _httpClient.GetAsync(EndpointMachinesDetails);

        if (response.StatusCode != HttpStatusCode.OK)
        {
            LastResponseStatus = response.StatusCode;
            return null;
        }
        response.EnsureSuccessStatusCode();

        //string error = await response.Content.ReadAsStringAsync();

        return await response.Content.ReadFromJsonAsync<Machine[]>();
    }

}
public class WeatherForecast
{
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public string? Summary { get; set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}