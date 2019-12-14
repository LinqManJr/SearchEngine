using SearchEngine.Core.Configurations;

namespace SearchEngine.Tests
{
    public static class DefaultConfigs
    {
        public static YandexSearchOptions YaOptions => new YandexSearchOptions("yandex", "https://yandex.com/search/xml", "03.304041461:62374306f8f0c1938a6a26f0ce0511be", "johnybond32");
    
        public static GoogleSearchOptions GoogleOptions => new GoogleSearchOptions("google", "", "AIzaSyB3ex6PDKCy54J92_1rB0q1TBn1jbv43SU", "012320430393294220051:cxnkugrhcjf");

        public static SearchEngineOptions BingOptions => new SearchEngineOptions("bing", "https://api.cognitive.microsoft.com/bing/v7.0/search", "2efb912d79e84eb6820192d1805fb44b");
    }
}
