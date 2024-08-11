namespace BlazorRadzenMls.Models.TheMachine;

public class Client
{
    public Client() { }
    public void Initialize()
    {
        try
        {
            User = Environment.UserName;
            Machine = Environment.MachineName;
            Domain = Environment.UserDomainName;
            ///CurrentDirectory = Environment.CurrentDirectory;
            Path = Environment.ProcessPath;//CommandLine
        }
        catch (Exception ex)
        {
            User = "Error";
            Machine = ex.Message;
            Domain = string.Empty;
            Path = string.Empty;
        }
    }

    public string? User { get; set; }
    public string? Machine { get; set; }
    public string? Domain { get; set; }
    public string? Path { get; set; }
}