namespace BlazorRadzenMls.Models;

using System.Text.Json.Serialization;

public class Imot
{
    public string? Label { get; set; }
    public string? Location { get; set; }
    public string? Type { get; set; }
    public string? Link { get; set; }
    public int Price { get; set; }
    [JsonIgnore]
    public string? Domain => new Uri(Link!).Host;
}
public class ImotMongo : Imot
{
    [JsonPropertyName("_id")]
    public string? Id { get; set; }
}