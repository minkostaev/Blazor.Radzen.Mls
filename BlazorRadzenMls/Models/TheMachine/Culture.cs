namespace Forms.Wpf.Mls.Tools.Models.TheMachine;

using System.Globalization;

public class Culture
{
    public Culture() { }
    public Culture(bool initialize = false)
    {
        try
        {
            if (initialize)
            {
                Name = CultureInfo.CurrentCulture.Name;
                Description = CultureInfo.CurrentCulture.EnglishName;
                Separator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            }
        }
        catch (Exception) { }
    }

    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Separator { get; set; }
}