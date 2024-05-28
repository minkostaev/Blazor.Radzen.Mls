namespace Forms.Wpf.Mls.Tools.Models.TheMachine;

public class Machine
{
    public string? _id { get; set; }
    public string? Hash { get; set; }
    public Client? Client { get; set; }
    public Version? Version { get; set; }
    public Culture? Culture { get; set; }
    public Processor? Processor { get; set; }
    public List<Network>? Networks { get; set; }
    public Dictionary<string, string?>? Variables { get; set; }
}