namespace BlazorRadzenMls.Services;

using BlazorRadzenMls.Models;
using System.Diagnostics;

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

    public static Stopwatch TimerStart()
    {
        var stopwatch = new Stopwatch();
        //stopwatch.Restart();
        stopwatch.Start();
        return stopwatch;
    }
    public static long TimerStop(Stopwatch stopwatch)
    {
        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

}