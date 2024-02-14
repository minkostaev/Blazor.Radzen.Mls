﻿namespace BlazorRadzenMls.Models;

using Microsoft.AspNetCore.Components;
using System.Reflection;

public static class AppValues
{
    private static string? Version => Assembly.GetExecutingAssembly().GetName().Version?.ToString();
    public static string VersionClient => (string.IsNullOrEmpty(Version)) ? "0.0.0.0" : Version;

    public const string PageIcons = "Icons";
    public const string PageDogs = "Dogs";
    public const string PageOptions = "Options";

    public static string GitHubMy => "https://github.com/minkostaev/Blazor.Radzen.Mls";
    public static string GitHubRadzen => "https://github.com/radzenhq/radzen-blazor";
    public static string GitHubAKSoftware => "https://github.com/aksoftware98/multilanguages";
    public static string GitHubBlazored => "https://github.com/Blazored";
    public static string GitHubAuth0 => "https://github.com/auth0";

    //public const string RolesAll = "admin, manager, user";
    //public const string RolesManager = "admin, manager";
    //public const string RolesAdmin = "admin";

    public static string GetAuth0(string address)
    {
        if (address.Contains(".github.io"))
            return "Auth0GitHub";
        else if (address.Contains(".onrender.com"))
            return "Auth0Render";
        else if (address.Contains(".netlify.app"))
            return "Auth0Netlify";
        else
            return "Auth0";
    }
    public static string GetGitHubSub(NavigationManager nav)
    {
        if (nav.BaseUri.Contains(".github."))
        {
            string subPage = nav.Uri[nav.BaseUri.Length..];
            var words = subPage.Split("/");
            if (words.Length > 0)
                return words[0];
        }
        return string.Empty;
    }

}
// to see
//https://blazor-university.com/

// Built-in Components:
// App
// Router
// DynamicComponent
// ErrorBoundary
// NavMenu
// NavLink

// EditForm

//StateHasChanged();
//await InvokeAsync(StateHasChanged);