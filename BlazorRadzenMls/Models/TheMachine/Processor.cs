namespace BlazorRadzenMls.Models.TheMachine;

public class Processor
{
    public Processor() { }
    public void Initialize()
    {
        try
        {
            Os64 = Environment.Is64BitOperatingSystem;
            Process64 = Environment.Is64BitProcess;
            Count = Environment.ProcessorCount;
        }
        catch (Exception)
        {
            Os64 = false;
            Process64 = false;
            Count = -1;
        }
    }

    public bool Os64 { get; set; }
    public bool Process64 { get; set; }
    public int Count { get; set; }
}