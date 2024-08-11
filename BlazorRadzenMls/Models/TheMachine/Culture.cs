namespace BlazorRadzenMls.Models.TheMachine;

using System.Globalization;

public class Culture
{
    public Culture() { }
    public void Initialize()
    {
        try
        {
            Name = CultureInfo.CurrentCulture.Name;
            Description = CultureInfo.CurrentCulture.EnglishName;
            Separator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
        }
        catch (Exception ex)
        {
            Name = "Error";
            Description = ex.Message;
            Separator = ",";
        }
    }

    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Separator { get; set; }
}