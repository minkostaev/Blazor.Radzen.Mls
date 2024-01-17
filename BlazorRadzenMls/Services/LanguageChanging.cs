namespace BlazorRadzenMls.Services;

using AKSoftware.Localization.MultiLanguages;
using System.Globalization;

public class LanguageChanging
{
    public LanguageChanging()
    {
        Languages = new Dictionary<string, string>();
        Languages.Add(en_US);
        Languages.Add(bg_BG);
    }
    public IDictionary<string, string>? Languages { get; set; }
    // flgs - https://icons8.com/icon/set/flags/fluency
    public KeyValuePair<string, string> en_US = new("en-US", "english");
    public KeyValuePair<string, string> bg_BG = new("bg-BG", "bulgarian");


    public void ChangeLanguage(ILanguageContainerService lang, string? language)
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
        lang.SetLanguage(CultureInfo.GetCultureInfo(id));
    }
    

}
// https://akmultilanguages.azurewebsites.net/

// in Program.cs add
//builder.Services.AddLanguageContainer<EmbeddedResourceKeysProvider>(Assembly.GetExecutingAssembly(), "Languages");
// or
//builder.Services.AddLanguageContainer(Assembly.GetExecutingAssembly(), CultureInfo.GetCultureInfo("en-US"), "Languages");

// add folder in the project with yml files with the translations
// /Languages/en-US.yml