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
    //public DateTime DateOnly { get { return DateTime.Date; } }

    public string? ClientName
    {
        get
        {
            string? result = null;
            var clients = Client?.Split('|');
            if (clients != null && clients.Length > 0)
                result = clients[0];
            return result;
        }
    }
    public string? ClientVersion
    {
        get
        {
            string? result = null;
            var clients = Client?.Split('|');
            if (clients != null && clients.Length > 1)
                result = clients[1];
            return result;
        }
    }

    public string? ServerName
    {
        get
        {
            string? result = null;
            var servers = Server?.Split('|');
            if (servers != null && servers.Length > 0)
                result = servers[0];
            return result;
        }
    }
    public string? ServerVersion
    {
        get
        {
            string? result = null;
            var servers = Server?.Split('|');
            if (servers != null && servers.Length > 1)
                result = servers[1];
            return result;
        }
    }

}