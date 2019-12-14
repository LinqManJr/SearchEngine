using NUnit.Framework;
using SearchEngine.Core.Configurations;
using SearchEngine.Core.Engines;
using SearchEngine.Core.Services;
using SearchEngine.WebApp.Services;
using System.Threading.Tasks;

namespace SearchEngine.Test
{
    [TestFixture]
    public class SearchServiceTest
    {
        private SearchConfig _yaConfig;
        private SearchConfig _bingConfig;
        private SearchConfig _googleConfig;

        private ISearchEngine _yaEngine;
        private ISearchEngine _googleEngine;
        private ISearchEngine _bingEngine;

        [SetUp]
        public void Setup()
        {
            _yaConfig = new SearchConfig
            {
                ApiKey = "03.304041461:62374306f8f0c1938a6a26f0ce0511be",
                UserName = "johnybond32",
                Url = "https://yandex.com/search/xml",
                SearchEngine = "Yandex"
            };
            _googleConfig = new SearchConfig { ApiKey = "AIzaSyB3ex6PDKCy54J92_1rB0q1TBn1jbv43SU", AppId = "012320430393294220051:cxnkugrhcjf", SearchEngine = "Google" };
            _bingConfig = new SearchConfig { ApiKey = "2efb912d79e84eb6820192d1805fb44b", Url = "https://api.cognitive.microsoft.com/bing/v7.0/search", SearchEngine = "Bing" };

            _yaEngine = new YandexSearchEngine(_yaConfig);
            _googleEngine = new GoogleSearchEngine(_googleConfig);
            _bingEngine = new BingSearchEngine(_bingConfig);

        }
        [Test]
        public async Task ShouldReturnFirstExecTask()
        {
            var searchService = new SearchService(_bingEngine);
            var result = await searchService.SearchInManyAsync("yandex");
            
            Assert.That(result.Error == null);
        }
    }
}
