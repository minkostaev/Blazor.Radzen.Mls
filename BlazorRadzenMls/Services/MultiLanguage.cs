namespace BlazorRadzenMls.Services;

using AKSoftware.Localization.MultiLanguages;
using System.Globalization;

public static class MultiLanguage
{
    public static IDictionary<string, string> Languages
    {
        get
        {
            return new Dictionary<string, string>
            {
                { en_US.Key, en_US.Value },
                { bg_BG.Key, bg_BG.Value }
            };
        }
    }
    // flgs - https://icons8.com/icon/set/flags/fluency
    public static readonly KeyValuePair<string, string> en_US = new("en-US", "english");
    public static readonly KeyValuePair<string, string> bg_BG = new("bg-BG", "bulgarian");
    

    public static void ChangeLanguage(ILanguageContainerService lang, string? language)
    {
        string id = string.Empty;
        if (Languages != null)
        {
            id = Languages.FirstOrDefault(x => x.Value == language).Key;
            if (Languages.Count > 0 && (string.IsNullOrEmpty(id)))
                id = Languages.FirstOrDefault().Key;
        }
        if (string.IsNullOrEmpty(id))
            id = "en-US";
        lang?.SetLanguage(CultureInfo.GetCultureInfo(id));
    }

}
// https://akmultilanguages.azurewebsites.net/

// in Program.cs add
//builder.Services.AddLanguageContainer<EmbeddedResourceKeysProvider>(Assembly.GetExecutingAssembly(), "Languages");
// or
//builder.Services.AddLanguageContainer(Assembly.GetExecutingAssembly(), CultureInfo.GetCultureInfo("en-US"), "Languages");

// add folder in the project with yml files with the translations
// /Languages/en-US.yml