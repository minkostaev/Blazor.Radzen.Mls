namespace BlazorRadzenMls.Services;

using BlazorRadzenMls.Models;
using System.Net;

public class IpService(IHttpClientFactory httpClientFactory)// to do interdace and use
{
    private readonly HttpClient _http = httpClientFactory.CreateClient("IpApi");
    private const HttpStatusCode HttpCodeJson = HttpStatusCode.Conflict;
    public async Task<Response> GetIp()
    {
        var (response, result) = await AppStatic.GetResponse(_http, "/");

        if (response == null)
            return result;

        var timer = AppStatic.TimerStart();

        try { result.Result = await response.Content.ReadAsStringAsync(); }
        catch { result.Status = HttpCodeJson; }

        result.DeserializeTime = AppStatic.TimerStop(timer);

        return result;
    }
}