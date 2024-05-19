namespace BlazorRadzenMls.Models;

public class MachinesLogs
{
    public string? _id { get; set; }
    public string? Hash { get; set; }
    public string? Value { get; set; }
    public string? Api { get; set; }
    public string? Desktop { get; set; }
    public string? Date { get; set; }
    public DateTime DateTime { get { return DateTime.Parse(Date!); } }
    public DateTime DateOnly { get { return DateTime.Date; } }
}