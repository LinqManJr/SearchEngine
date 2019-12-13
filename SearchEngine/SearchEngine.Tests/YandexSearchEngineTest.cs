using NUnit.Framework;
using SearchEngine.Core.Configurations;
using SearchEngine.Core.Engines;
using System.Threading.Tasks;

namespace SearchEngine.Test
{
    public class YandexSearchEngineTest
    {
        private SearchConfig config;
        
        [SetUp]
        public void Setup()
        {
            config = new SearchConfig
            {
                ApiKey = "03.304041461:62374306f8f0c1938a6a26f0ce0511be",
                UserName = "johnybond32",
                Url = "https://yandex.com/search/xml"
            };
        }

        [Test]
        public void ShouldReturnResult()
        {
            var searchEngine = new YandexSearchEngine(config);
            var result = searchEngine.Search("yandex");

            Assert.IsNull(result.Error);
            Assert.IsTrue(result.Results.Count > 0);
        }

        [Test]
        public void ShouldReturnErrorResultIfInvalidSearch()
        {
            config.Url = "https://yandex.ru/search/xml";
            var searchEngine = new YandexSearchEngine(config);
            var result = searchEngine.Search("yandex");

            Assert.IsNotNull(result.Error);
            Assert.That(result.Error.Title == "Error Code is 48");
            Assert.That(result.Error.Description.StartsWith("�������� ��� ������"));
            
        }

        [Test]
        public void ShouldReturnErrorResultIfInvalidApiKeyOrUserName()
        {
            config.ApiKey = "03.304041461:62374326f8f0c193806a26f0cc0511be";
            var searchEngine = new YandexSearchEngine(config);
            var result = searchEngine.Search("yandex");

            Assert.IsNotNull(result.Error);
            Assert.That(result.Error.Title == "Error Code is 42");
            Assert.That(result.Error.Description.StartsWith("Invalid key"));
        }        

        [Test]
        public async Task ShouldReturnResulAsync()
        {
            var searchEngine = new YandexSearchEngine(config);
            var result = await searchEngine.SearchAsync("yandex");

            Assert.IsNull(result.Error);
            Assert.IsTrue(result.Results.Count > 0);
        }
    }
}