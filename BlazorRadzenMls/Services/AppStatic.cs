namespace BlazorRadzenMls.Services;

using BlazorRadzenMls.Models;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Net.Http;
using System.Net;
using System.Text.RegularExpressions;

public static class AppStatic
{

    public static string FormatMilliseconds(long milliseconds)
    {
        TimeSpan span = TimeSpan.FromMilliseconds(milliseconds);
        if (span.TotalDays >= 1)
            return $"{(int)span.TotalDays} d {span:hh\\:mm\\:ss\\.fff}";
        else if (span.TotalHours >= 1)
            return $"{(int)span.TotalHours} h {span:mm\\:ss\\.fff}";
        else if (span.TotalMinutes >= 1)
            return $"{(int)span.TotalMinutes} m {span:ss\\.fff}";
        else
            return $"{span.TotalSeconds:0.###} s";
    }

    public static string GetAuth0(string address)
    {
        if (address.Contains(AppValues.GitHubDomain))
            return "Auth0GitHub";
        else if (address.Contains(AppValues.RenderDomain))
            return "Auth0Render";
        else if (address.Contains(AppValues.NetlifyDomain))
            return "Auth0Netlify";
        else
            return "Auth0";
    }

    public static string GetGitHubSub(NavigationManager nav)
    {
        if (nav.BaseUri.Contains(AppValues.GitHubDomain))
        {
            var words = nav.BaseUri.Split("/");
            bool isGitHub = false;
            foreach (var word in words)
            {
                if (isGitHub)
                    return "/" + word;
                if (word.Contains(AppValues.GitHubDomain))
                    isGitHub = true;
            }
        }
        return string.Empty;
    }

    public static Stopwatch TimerStart()
    {
        var stopwatch = new Stopwatch();
        ///stopwatch.Restart();
        stopwatch.Start();
        return stopwatch;
    }
    public static long TimerStop(Stopwatch stopwatch)
    {
        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    public static async Task<(HttpResponseMessage?, Response)> GetResponse(HttpClient http, string uri)
    {
        var result = new Response();
        var timer = TimerStart();

        result.Status = HttpStatusCode.InternalServerError;
        HttpResponseMessage? response = null;
        try { response = await http.GetAsync(uri); }
        catch { result.Status = HttpStatusCode.InternalServerError; }

        if (response == null)
            return (null, result);
        else
            result.Status = response.StatusCode;

        if (response.StatusCode != HttpStatusCode.OK)
            return (null, result);

        response.EnsureSuccessStatusCode();

        result.RequestTime = TimerStop(timer);

        return (response, result);
    }

    public static string WindowsBuilds(string? buildNumber)
    {
        if (string.IsNullOrEmpty(buildNumber))
            return string.Empty;
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

    public static string GetImagePath(string language, MultiLanguage ml)
    {
        string fileName = string.Empty;
        if (language == ml.en_US.Value)
        {
            fileName = "usa";
        }
        else if (language == ml.bg_BG.Value)
        {
            fileName = "bulgaria";
        }
        if (!string.IsNullOrEmpty(fileName))
            return $"images/{fileName}-48.png";
        else
            return string.Empty;
    }

    public static string FirstLetterToUpper(string? str)
    {
        if (string.IsNullOrEmpty(str))
            return string.Empty;
        return char.ToUpper(str[0]) + str.Substring(1);
    }

}