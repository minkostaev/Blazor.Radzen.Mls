namespace BlazorRadzenMls.Models;

using System.Reflection;

public static class AppValues
{
    public static string Version => Assembly.GetExecutingAssembly().GetName().Version.ToString();

    //public const string RolesAll = "admin, manager, user";
    //public const string RolesManager = "admin, manager";
    //public const string RolesAdmin = "admin";

    public const string PageIcons = "Icons";
    public const string PageDogs = "Dogs";
    public const string PageOptions = "Options";

}