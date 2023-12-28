namespace BlazorRadzenMls.Models;

using System.Reflection;

public static class AppValues
{
    public static string Version => Assembly.GetExecutingAssembly().GetName().Version.ToString();

    public const string RolesAll = "admin, manager, user";
    public const string RolesManager = "admin, manager";
    public const string RolesAdmin = "admin";

    public const string PageBase = "analysis";
    public const string PageBalanceSheet = "balancesheet";
    public const string PageInlineViewer = "inlineviewer";
    public const string PagePivotViewer = "pivotviewer";

}