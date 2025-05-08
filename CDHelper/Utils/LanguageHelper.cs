using System.Globalization;

namespace CDHelper.Utils
{
    public static class LanguageHelper
    {
        public static List<string> allowedLanguages = new()
        {
            "pt",  // Portuguese (Brazil)
            "com.br",  // Portuguese (Brazil)
            
            "en",     // English
            "com",     // English
            
            "es",     // Spanish
            "fi",     // Finnish
            "it",     // Italian
            "nl",     // Dutch
            "de",     // German
            "fr",     // French
            "tr"      // Turkish
        };


        public static void SetLanguage(string domain)
        {
            string cultureCode;

            if (domain == "com.br" || domain == "pt")
                cultureCode = "pt-BR";
            else if (domain == "com" || domain == "en")
                cultureCode = "en";
            else if (domain == "es")
                cultureCode = "es";
            else if (domain == "fi")
                cultureCode = "fi";
            else if (domain == "it")
                cultureCode = "it";
            else if (domain == "nl")
                cultureCode = "nl";
            else if (domain == "de")
                cultureCode = "de";
            else if (domain == "fr")
                cultureCode = "fr";
            else if (domain == "com.tr")
                cultureCode = "tr";
            else
                cultureCode = "en"; //fallback

            SetThreadLanguage(cultureCode);
        }

        public static string Get(string resourceValue)
        {
            return resourceValue.Replace("\\n", Environment.NewLine);
        }

        public static string Get(string resourceValue, params object[] args)
        {
            var formatted = string.Format(resourceValue, args);
            return formatted.Replace("\\n", Environment.NewLine);
        }

        private static void SetThreadLanguage(string cultureCode)
        {
            CultureInfo culture = new CultureInfo(cultureCode);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }

    }
}
