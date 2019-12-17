using NUnit.Framework;
using SearchEngine.Core.Configurations;
using SearchEngine.Core.Engines;
using SearchEngine.Tests;
using System.Threading.Tasks;

namespace SearchEngine.Test
{
    public class YandexSearchEngineTest
    {        
        private YandexSearchOptions _options;

        [SetUp]
        public void Setup()
        {
            _options = DefaultConfigs.YaOptions;
        }

        [Test]
        public void ShouldReturnResult()
        {
            var searchEngine = new YandexSearchEngine(_options);
            var result = searchEngine.Search("nginx");

            Assert.IsNull(result.Error);
            Assert.IsTrue(result.Results.Count > 0);
        }

        [Test]
        public void ShouldReturnErrorResultIfInvalidSearch()
        {
            _options.Uri = "https://yandex.ru/search/xml";
            var searchEngine = new YandexSearchEngine(_options);
            var result = searchEngine.Search("nginx");

            Assert.IsNotNull(result.Error);
            Assert.That(result.Error.Title == "Error Code is 48");
            Assert.That(result.Error.Description.StartsWith("Неверный тип поиска"));
            
        }

        [Test]
        public void ShouldReturnErrorResultIfInvalidApiKeyOrUserName()
        {
            _options.Apikey = "03.304041461:62374326f8f0c193806a26f0cc0511be";
            var searchEngine = new YandexSearchEngine(_options);
            var result = searchEngine.Search("nginx");

            Assert.IsNotNull(result.Error);
            Assert.That(result.Error.Title == "Error Code is 42");
            Assert.That(result.Error.Description.StartsWith("Invalid key"));
        }        

        [Test]
        public async Task ShouldReturnResultAsync()
        {
            var searchEngine = new YandexSearchEngine(_options);
            var result = await searchEngine.SearchAsync("nginx");

            Assert.IsNull(result.Error);
            Assert.IsTrue(result.Results.Count > 0);
        }
    }
}