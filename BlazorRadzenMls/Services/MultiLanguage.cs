namespace BlazorRadzenMls.Services;

using AKSoftware.Localization.MultiLanguages;
using System.Globalization;

public class MultiLanguage
{
    private readonly ILanguageContainerService _languageService;
    private readonly AppState _appState;
    public MultiLanguage(ILanguageContainerService languageService, AppState appState)
    {
        _languageService = languageService;
        _appState = appState;
        Languages = new Dictionary<string, string>
        {
            { en_US.Key, en_US.Value },
            { bg_BG.Key, bg_BG.Value }
        };
    }

    public IDictionary<string, string> Languages { get; private set; }
    // flgs - https://icons8.com/icon/set/flags/fluency
    public readonly KeyValuePair<string, string> en_US = new("en-US", "english");
    public readonly KeyValuePair<string, string> bg_BG = new("bg-BG", "bulgarian");

    public string Get(string key) { return _languageService[key]; }

    /// <summary>
    /// Change Language
    /// </summary>
    /// <param name="language">Language code (ex: en-US)</param>
    /// <param name="saveLocal"></param>
    /// <returns></returns>
    public async Task<bool> ChangeLanguage(string? language, bool saveLocal = false)
    {
        if (_languageService.CurrentCulture.Name == language)
            return false;
        string id = string.Empty;
        bool success = false;
        if (Languages != null)
        {
            id = Languages.FirstOrDefault(x => x.Value == language).Key;
            if (Languages.Count > 0 && (string.IsNullOrEmpty(id)))
            {
                id = Languages.FirstOrDefault().Key;
                success = true;
            }
        }
        if (string.IsNullOrEmpty(id))
        {
            id = "en-US";
        }
        _languageService?.SetLanguage(CultureInfo.GetCultureInfo(id));
        #region AppState
        _appState.SiteOptions.Language = Languages?[id];
        if (saveLocal)
        {
            await _appState.SaveAppOptions();
        }
        #endregion
        return success;
    }
}
// https://akmultilanguages.azurewebsites.net/

// in Program.cs add
//builder.Services.AddLanguageContainer<EmbeddedResourceKeysProvider>(Assembly.GetExecutingAssembly(), "Languages");
// or
//builder.Services.AddLanguageContainer(Assembly.GetExecutingAssembly(), CultureInfo.GetCultureInfo("en-US"), "Languages");

// add folder in the project with yml files with the translations
// /Languages/en-US.yml