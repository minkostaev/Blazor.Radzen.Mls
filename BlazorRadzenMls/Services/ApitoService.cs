namespace BlazorRadzenMls.Services;

using BlazorRadzenMls.Contracts;
using BlazorRadzenMls.Models;
using Mintzat.Email.Models.TheMachine;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

public class ApitoService : IApitoService
{
    private const string EndpointMachinesDetails = "/machinesdetails";
    private const string EndpointMachinesLogs = "/machineslogs";
    private const string EndpointMachinesRecords = "/machinesrecords";
    private const string EndpointImoti = "/imoti";
    private const string EndpointEmailresend = "/emailresend";

    private const HttpStatusCode HttpCodeJson = HttpStatusCode.Conflict;

    private readonly StateService _state;
    private readonly HttpClient _httpSomee;
    private readonly HttpClient _httpRender;
    private HttpClient ApitoClient =>
        _state.SiteOptions.ApitoId switch { 0 => _httpRender, 1 => _httpSomee, _ => _httpRender, };
    public ApitoService(IHttpClientFactory httpClientFactory, StateService state)
    {
        _state = state;
        _httpSomee = httpClientFactory.CreateClient("ApitoSomee");
        _httpRender = httpClientFactory.CreateClient("ApitoRender");
    }

    public async Task<Response> GetMachinesLogs()
    {
        var (response, result) = await AppStatic.GetResponse(ApitoClient, EndpointMachinesLogs);

        if (response == null)
            return result;

        var timer = AppStatic.TimerStart();

        try { result.Result = await response.Content.ReadFromJsonAsync<MachinesLogs[]>(); }
        catch { result.Status = HttpCodeJson; }

        result.DeserializeTime = AppStatic.TimerStop(timer);

        return result;
    }
    public async Task<Response> DeleteMachinesLogs(string[]? ids)
    {
        var result = new Response();
        if (ids == null)
        {
            result.Status = HttpStatusCode.ExpectationFailed;
            return result;
        }

        var timer = AppStatic.TimerStart();

        HttpRequestMessage? httpRequest;
        try
        {
            string jsonString = JsonSerializer.Serialize(ids);
            string requestBody = "{\"Ids\": " + jsonString + "}";
            httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(string.Concat(ApitoClient.BaseAddress!.AbsoluteUri, EndpointMachinesLogs.AsSpan(1))),
                Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
            };
        }
        catch
        {
            result.Status = HttpCodeJson;
            return result;
        }

        HttpResponseMessage? httpResponse;
        try { httpResponse = await ApitoClient.SendAsync(httpRequest!); }
        catch
        {
            result.Status = HttpStatusCode.InternalServerError;
            return result;
        }

        if (httpResponse.StatusCode != HttpStatusCode.NoContent)
        {
            result.Status = httpResponse.StatusCode;
            return result;
        }

        httpResponse.EnsureSuccessStatusCode();

        result.RequestTime = AppStatic.TimerStop(timer);
        result.Result = true;

        return result;
    }

    public async Task<Response> GetMachinesDetails()
    {
        var (response, result) = await AppStatic.GetResponse(ApitoClient, EndpointMachinesDetails);

        if (response == null)
            return result;
         
        var timer = AppStatic.TimerStart();

        try { result.Result = await response.Content.ReadFromJsonAsync<MachineMongo[]>(); }
        catch { result.Status = HttpCodeJson; }

        result.DeserializeTime = AppStatic.TimerStop(timer);

        return result;
    }

    public async Task<Response> GetMachinesRecords()
    {
        var (response, result) = await AppStatic.GetResponse(ApitoClient, EndpointMachinesRecords);

        if (response == null)
            return result;

        var timer = AppStatic.TimerStart();

        try { result.Result = await response.Content.ReadFromJsonAsync<MachinesRecords[]>(); }
        catch { result.Status = HttpCodeJson; }

        result.DeserializeTime = AppStatic.TimerStop(timer);

        return result;
    }

    public async Task<Response> GetImoti()
    {
        var (response, result) = await AppStatic.GetResponse(ApitoClient, EndpointImoti);

        if (response == null)
            return result;

        var timer = AppStatic.TimerStart();

        try { result.Result = await response.Content.ReadFromJsonAsync<ImotMongo[]>(); }
        catch { result.Status = HttpCodeJson; }

        result.DeserializeTime = AppStatic.TimerStop(timer);

        return result;
    }
    public async Task<Response> PostImoti(ImotMongo item, bool put = false)
    {
        var result = new Response();
        var timer1 = AppStatic.TimerStart();

        HttpResponseMessage response;
        if (!put)
            response = await ApitoClient.PostAsJsonAsync(EndpointImoti, item as Imot);
        else
        {
            string? id = item?.Id;
            Imot? imot = item;
            response = await ApitoClient.PutAsJsonAsync($"{EndpointImoti}/{id}", imot);
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
        catch { result.Status = HttpCodeJson; }

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

        HttpResponseMessage? response = null;
        try { response = await ApitoClient.DeleteAsync($"{EndpointImoti}/{id}"); }
        catch { result.Status = HttpCodeJson; }

        if (response == null)
        {
            result.Status = HttpStatusCode.InternalServerError;
            return result;
        }

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


    public async Task<Response> PostEmail(string from, string name, string topic, string message)
    {
        var result = new Response();
        var timer1 = AppStatic.TimerStart();

        var email = new
        {
            from,
            name,
            topic,
            message,
            secondTopic = "Summary and confirmation on message you sent",
            secondHeader = "<h3>You have send this message to us</h3>",
            secondFooter = "<h4>We'll get back to you as soon as possible</h4>"
        };

        HttpResponseMessage response = await ApitoClient.PostAsJsonAsync(EndpointEmailresend, email);
        
        if (response == null)
        {
            result.Status = HttpStatusCode.InternalServerError;
            return result;
        }
        else
            result.Status = response.StatusCode;

        result.RequestTime = AppStatic.TimerStop(timer1);

        if (response.StatusCode == HttpStatusCode.Created)
        {
            result.Result = true;
            return result;
        }
        else
            return result;
    }


}