namespace Forms.Wpf.Mls.Tools.Models.TheMachine;

public class Client
{
    public Client() { }
    public Client(bool initialize = false)
    {
        if (initialize)
        {
            User = Environment.UserName;
            Machine = Environment.MachineName;
            Domain = Environment.UserDomainName;
            //CurrentDirectory = Environment.CurrentDirectory;
            Path = Environment.ProcessPath;//CommandLine
        }
    }

    public string? User { get; set; }
    public string? Machine { get; set; }
    public string? Domain { get; set; }
    public string? Path { get; set; }
}