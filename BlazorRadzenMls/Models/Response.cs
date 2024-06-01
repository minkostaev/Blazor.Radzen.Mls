namespace BlazorRadzenMls.Models;

using System.Net;

public class Response
{
    public HttpStatusCode Status { get; set; }
    public object? Result { get; set; }
    public long RequestTime { get; set; }
    public long DeserializeTime { get; set; }
}