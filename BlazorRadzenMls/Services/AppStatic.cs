﻿namespace BlazorRadzenMls.Services;

using BlazorRadzenMls.Models;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Net.Http;
using System.Net;
using System.Text.RegularExpressions;
using BlazorRadzenMls.Contracts;
using Radzen;
using Microsoft.AspNetCore.Components.Authorization;

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

    public static string GetImagePath(string language, LanguageService ml)
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

    public static async Task Refresh(IVersioningService __ver, DialogService __dialog, LanguageService __ml)
    {
        var options = new ConfirmOptions() { OkButtonText = __ml["Refresh"], CancelButtonText = __ml["Cancel"] };
        bool? confirm = await __dialog.Confirm(__ml["Refresh1"], __ml["Refresh"], options);
        if (confirm == true)
            await __ver.Reload();
    }

    public static string IconColor(bool useColor) => useColor ? Colors.Success : string.Empty;//Info
    public static string IconColorStyle(bool useColor) => useColor ? $"color: {IconColor(useColor)};" : string.Empty;

    ///public string ReplaceFirst(string text, string oldValue, string newValue)
    ///{
    ///    int position = text.IndexOf(oldValue);
    ///    if (position < 0) { return text; }
    ///    text = text.Substring(0, position) + newValue + text.Substring(position + oldValue.Length);
    ///    return text;
    ///}

    public static async Task<string?> GetLoggedEmail(AuthenticationStateProvider auth)
    {
        var authstate = await auth.GetAuthenticationStateAsync();
        var user = authstate.User;
        return user?.Identity?.Name;
    }

}