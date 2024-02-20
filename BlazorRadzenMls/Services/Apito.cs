namespace BlazorRadzenMls.Services;

using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
using System.Text;
using BlazorRadzenMls.Contracts;

public class Apito : IApito
{
    public HttpStatusCode LastResponseStatus { get; set; } = HttpStatusCode.OK;

    //public string LastResponseMessage { get; set; }

    private const string EndpointTest = "/weatherforecast";

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

}
public class WeatherForecast
{
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public string? Summary { get; set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}