using SearchEngine.Core.Configurations;

namespace SearchEngine.Core.Test
{
    public static class DefaultConfigs
    {
        public static YandexSearchOptions YaOptions => new YandexSearchOptions
        {
            Name = "yandex",
            Uri = "https://yandex.com/search/xml",
            Apikey = "",
            Username = ""
        };

        public static GoogleSearchOptions GoogleOptions => new GoogleSearchOptions
        {
            Name = "google",
            Uri = string.Empty,
            Apikey = "",
            AppId = ""
        };

        public static SearchEngineOptions BingOptions => new SearchEngineOptions
        {
            Name = "bing",
            Uri = "https://api.cognitive.microsoft.com/bing/v7.0/search",
            Apikey = ""
        };        
    }
}
