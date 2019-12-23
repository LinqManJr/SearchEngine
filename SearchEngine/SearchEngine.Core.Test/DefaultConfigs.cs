using SearchEngine.Core.Configurations;
using SearchEngine.Core.Models;
using System.Collections.Generic;

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
            Apikey = "AIzaSyB3ex6PDKCy54J92_1rB0q1TBn1jbv43SU",
            AppId = "012320430393294220051:cxnkugrhcjf"
        };

        public static SearchEngineOptions BingOptions => new SearchEngineOptions
        {
            Name = "bing",
            Uri = "https://api.cognitive.microsoft.com/bing/v7.0/search",
            Apikey = "2efb912d79e84eb6820192d1805fb44b"
        };        
    }
}
