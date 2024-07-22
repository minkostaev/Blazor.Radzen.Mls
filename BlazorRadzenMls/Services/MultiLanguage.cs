namespace BlazorRadzenMls.Services;

using AKSoftware.Localization.MultiLanguages;
using System.Globalization;

public class MultiLanguage
{
    private readonly ILanguageContainerService _language;
    private readonly AppState __state;
    public MultiLanguage(ILanguageContainerService languageService, AppState appState)
    {
        _language = languageService;
        __state = appState;
        Languages = new Dictionary<string, string>
        {
            { en_US.Key, en_US.Value },
            { bg_BG.Key, bg_BG.Value }
        };
    }

    /// <summary>
    /// Languages list
    /// </summary>
    public IDictionary<string, string> Languages { get; private set; }
    public readonly KeyValuePair<string, string> en_US = new("en-US", "english");
    public readonly KeyValuePair<string, string> bg_BG = new("bg-BG", "bulgarian");
    // flgs - https://icons8.com/icon/set/flags/fluency

    /// <summary>
    /// CultureInfo CurrentCulture
    /// </summary>
    public CultureInfo CurrentCulture => _language.CurrentCulture;

    /// <summary>
    /// Get language value from the yml
    /// </summary>
    /// <param name="key">Key from the yml file</param>
    /// <returns>Language string value from the yml</returns>
    public string this[string? key] => _language[key];

    /// <summary>
    /// Change language
    /// </summary>
    /// <param name="language">Language code (ex: en-US)</param>
    /// <param name="saveLocal">Save to local storage</param>
    /// <returns>Success status</returns>
    public async Task<bool> ChangeLanguage(string? language, bool saveLocal = false)
    {
        if (CurrentCulture.Name == language)
            return false;
        string id = string.Empty;
        bool success = false;
        if (Languages != null)
        {
            id = Languages.FirstOrDefault(x => x.Value == language).Key;
            if (Languages.Count > 0 && string.IsNullOrEmpty(id))
            {
                id = Languages.FirstOrDefault().Key;
                success = true;
            }
        }
        if (string.IsNullOrEmpty(id))
        {
            id = "en-US";
        }
        _language?.SetLanguage(CultureInfo.GetCultureInfo(id));
        #region AppState
        __state.SiteOptions.Language = Languages?[id];
        if (saveLocal)
        {
            await __state.SaveAppOptions();
        }
        #endregion
        return success;
    }
}
// https://akmultilanguages.azurewebsites.net/

// in Program.cs add:
//builder.Services.AddLanguageContainer<EmbeddedResourceKeysProvider>(Assembly.GetExecutingAssembly(), "Languages");
// or
//builder.Services.AddLanguageContainer(Assembly.GetExecutingAssembly(), CultureInfo.GetCultureInfo("en-US"), "Languages");

// add folder in the project with yml files with the translations
// /Languages/en-US.yml

// in project file
//<ItemGroup>
//  <EmbeddedResource Include="Languages\en-US.yml" />
//</ItemGroup>