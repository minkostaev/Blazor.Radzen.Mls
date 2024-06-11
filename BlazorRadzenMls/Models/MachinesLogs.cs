namespace BlazorRadzenMls.Models;

using System.Text.Json.Serialization;

public class MachinesLogs
{
    [JsonPropertyName("_id")]
    public string? Id { get; set; }
    public string? Hash { get; set; }
    public string? Value { get; set; }
    public string? Api { get; set; }
    public string? Desktop { get; set; }
    public string? Date { get; set; }

    // more
    public DateTime DateTime { get { return DateTime.Parse(Date!); } }
    public DateTime DateOnly { get { return DateTime.Date; } }
}
public class MachinesRecords
{
    [JsonPropertyName("_id")]
    public string? Id { get; set; }
    public string? Hash { get; set; }
    public string? Value { get; set; }
    public string? Client { get; set; }
    public string? Server { get; set; }
    public string? Date { get; set; }

    // more
    public DateTime DateTime { get { return DateTime.Parse(Date!); } }
    public DateTime DateOnly { get { return DateTime.Date; } }
}