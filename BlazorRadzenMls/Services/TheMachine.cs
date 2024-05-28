namespace BlazorRadzenMls.Services;

using System.Text.RegularExpressions;

public static class TheMachine
{
    public static string WindowsBuilds(string buildNumber)
    {
        string pattern = @"\d+";
        var regex = new Regex(pattern);
        MatchCollection matches = regex.Matches(buildNumber);
        if (matches == null || matches.Count == 0
            || matches[0] == null || matches[0].Value == null)
            return "null";
        _ = int.TryParse(matches[0].Value, out int number);
        return number switch
        {
            >= 0 and <= 949 => "3",
            >= 950 and <= 1997 => "95",
            >= 1998 and <= 2194 => "98",
            >= 2195 and <= 2200 => "2000",
            >= 2201 and <= 2222 => "98 SE",
            >= 2223 and <= 2999 => "XP",
            >= 3000 and <= 3000 => "Me",
            >= 3001 and <= 4000 => "XP 64",
            >= 4001 and <= 6999 => "Vista",
            >= 7000 and <= 8999 => "7",
            >= 9000 and <= 9999 => "8",
            >= 10000 and <= 19999 => "10",
            >= 20000 and <= 29999 => "11",
            >= 30000 and <= 39999 => "12",
            _ => "unknown",
        };
    }

}