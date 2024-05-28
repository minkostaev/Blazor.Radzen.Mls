namespace Forms.Wpf.Mls.Tools.Models.TheMachine;

public class Version
{
    public string? Description { get; set; }
    public string? Platform { get; set; }
    public string? Pack { get; set; }
    public int? Build { get; set; }
    public int? Major { get; set; }
    //public int MajorRevision { get; private set; }
    public int? Minor { get; set; }
    //public int MinorRevision { get; private set; }
    public int? Revision { get; set; }
}