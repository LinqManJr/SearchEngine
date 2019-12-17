using NUnit.Framework;
using SearchEngine.Core.Configurations;
using SearchEngine.Core.Engines;
using SearchEngine.Tests;
using SearchEngine.WebApp.Services;
using System.Threading.Tasks;

namespace SearchEngine.Test
{
    [TestFixture]
    public class SearchServiceTest
    {
        private YandexSearchOptions _yaOptions;
        private SearchEngineOptions _bingOptions;
        private GoogleSearchOptions _googleOptions;

        private ISearchEngine _yaEngine;
        private ISearchEngine _googleEngine;
        private ISearchEngine _bingEngine;

        [SetUp]
        public void Setup()
        {
            _yaOptions = DefaultConfigs.YaOptions;
            _googleOptions = DefaultConfigs.GoogleOptions;
            _bingOptions = DefaultConfigs.BingOptions;

            _yaEngine = new YandexSearchEngine(_yaOptions);
            _googleEngine = new GoogleSearchEngine(_googleOptions);
            _bingEngine = new BingSearchEngine(_bingOptions);

        }
        [Test]
        public async Task ShouldReturnFirstExecTask()
        {
            var searchService = new SearchService(_bingEngine, _googleEngine);
            var result = await searchService.SearchInManyAsync("nginx");
            
            Assert.That(result.Error == null);
        }
    }
}
