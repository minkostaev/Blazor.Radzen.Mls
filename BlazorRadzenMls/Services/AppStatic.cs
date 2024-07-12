﻿namespace BlazorRadzenMls.Services;

using BlazorRadzenMls.Models;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Net.Http;
using System.Net;

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
        //stopwatch.Restart();
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

        HttpResponseMessage? response = null;
        try { response = await http.GetAsync(uri); }
        catch (Exception) { }

        if (response == null)
        {
            result.Status = HttpStatusCode.InternalServerError;
            return (null, result);
        }
        else
        {
            result.Status = response.StatusCode;
        }

        if (response.StatusCode != HttpStatusCode.OK)
        {
            return (null, result);
        }

        response.EnsureSuccessStatusCode();

        result.RequestTime = TimerStop(timer);

        return (response, result);
    }


}