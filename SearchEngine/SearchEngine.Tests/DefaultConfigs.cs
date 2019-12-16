using SearchEngine.Core.Configurations;

namespace SearchEngine.Tests
{
    public static class DefaultConfigs
    {
        public static YandexSearchOptions YaOptions => new YandexSearchOptions { Name = "yandex", Uri = "https://yandex.com/search/xml", Apikey = "03.304041461:62374306f8f0c1938a6a26f0ce0511be", Username = "johnybond32" };

        public static GoogleSearchOptions GoogleOptions => new GoogleSearchOptions { Name = "google", Uri = string.Empty, Apikey = "AIzaSyB3ex6PDKCy54J92_1rB0q1TBn1jbv43SU", AppId = "012320430393294220051:cxnkugrhcjf" };

        public static SearchEngineOptions BingOptions => new SearchEngineOptions { Name = "bing", Uri = "https://api.cognitive.microsoft.com/bing/v7.0/search", Apikey = "2efb912d79e84eb6820192d1805fb44b"};
    }
}
