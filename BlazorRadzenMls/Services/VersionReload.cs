namespace BlazorRadzenMls.Services;

using BlazorRadzenMls.Contracts;
using BlazorRadzenMls.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Reflection;
using System.Timers;

public class VersionReload : IVersionReload
{
    private readonly IJSRuntime __js;
    private readonly NavigationManager __nav;
    public VersionReload(IJSRuntime IJSRuntime, NavigationManager navigationManager)
    {
        __js = IJSRuntime;
        __nav = navigationManager;
        VersionProject = (Version == null) ? "0.0.0.0" : Version.ToString();
        VersionWwwroot = "0";
            Console.WriteLine(NeedUpdate);
        VersionWwwroot = VersionProject;

        var timerCheck = new Timer(1000) { Enabled = true };//1 sec
        timerCheck.Elapsed += async delegate
        {
            await CheckVersion();
            timerCheck.Stop();
        };

        var timer = new Timer(10000) { Enabled = true };//10 sec
        timer.Elapsed += async delegate
        {
            TimerEvent?.Invoke(timer, EventArgs.Empty);
            await CheckVersion();
            if (NeedUpdate)
            {
                DifferenceEvent?.Invoke(timer, EventArgs.Empty);
            }
        };
    }

    /// <summary>
    /// Project version number
    /// </summary>
    public static Version? Version => Assembly.GetExecutingAssembly().GetName().Version;

    /// <summary>
    /// Version number in project file
    /// </summary>
    public string VersionProject { get; private set; }

    /// <summary>
    /// Cached version numver
    /// </summary>
    public string VersionWwwroot { get; private set; }

    /// <summary>
    /// Is there differences in the versions (depends on CheckVersion methods)
    /// </summary>
    public bool NeedUpdate => !VersionProject.StartsWith(VersionWwwroot);

    /// <summary>
    /// Differences in the versions found event
    /// </summary>
    public event EventHandler? DifferenceEvent;

    /// <summary>
    /// Timer cycle event triggered every CheckInterval period
    /// </summary>
    public event EventHandler? TimerEvent;

    /// <summary>
    /// Calls js method that checks version number and determine whether is new or not
    /// </summary>
    /// <returns>The success status of the method</returns>
    public async Task CheckVersion()
    {
        VersionWwwroot = await __js.CheckVersion(__nav, this, "CheckVersion");
    }

    /// <summary>
    /// Calls js method that hard reload the site
    /// </summary>
    /// <returns>The success of reload</returns>
    public async Task<bool> Reload()
    {
        return await __js.Reload(this, "Reload");
    }
}
///for version check use js fetch text in /data/version.txt
/// const response = await fetch(path);
/// const resData = await response.text();
///version.txt is auto created with pre build script
///in project file
/// <Target Name="PreBuild" BeforeTargets="PreBuildEvent">
///   <Exec Command="powershell.exe -ExecutionPolicy Bypass -NoProfile -NonInteractive -File $(SolutionDir)preBuild.ps1" />
/// </Target>
///preBuild.ps1
/// $xml = [xml](Get-Content "BlazorRadzenMls.csproj")
/// $version = $xml.Project.PropertyGroup.Version
/// $version | Out-File -FilePath "wwwroot\data\version.txt" -Encoding utf8