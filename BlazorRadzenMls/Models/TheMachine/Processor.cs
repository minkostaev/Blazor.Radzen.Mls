namespace Forms.Wpf.Mls.Tools.Models.TheMachine;

public class Processor
{
    public Processor() { }
    public Processor(bool initialize = false)
    {
        if (initialize)
        {
            Os64 = Environment.Is64BitOperatingSystem;
            Process64 = Environment.Is64BitProcess;
            Count = Environment.ProcessorCount;
        }
    }

    public bool Os64 { get; set; }
    public bool Process64 { get; set; }
    public int Count { get; set; }
}