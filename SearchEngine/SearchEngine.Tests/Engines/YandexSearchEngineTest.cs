using NUnit.Framework;
using SearchEngine.Core.Configurations;
using SearchEngine.Core.Engines;
using System.Threading.Tasks;

namespace SearchEngine.Tests.Engines
{
    [TestFixture]
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
        public async Task ShouldReturnResultAsync()
        {
            var searchEngine = new YandexSearchEngine(_options);
            var result = await searchEngine.SearchAsync("nginx");

            Assert.IsNull(result.Error);
            Assert.IsTrue(result.Results.Count > 0);
        }

        [Test]
        [TestCase(8)]
        [TestCase(25)]
        public void ShouldReturnValidNumOfItems(int count)
        {
            _options.NumItems = count;
            var searchEngine = new YandexSearchEngine(_options);
            var result = searchEngine.Search("nginx");

            Assert.IsNull(result.Error);
            Assert.IsTrue(result.Results.Count == count);
        }

        [Test]
        [TestCase(8)]
        [TestCase(25)]
        public async Task ShouldReturnValidNumOfItemsAsync(int count)
        {
            _options.NumItems = count;
            var searchEngine = new YandexSearchEngine(_options);
            var result = await searchEngine.SearchAsync("nginx");

            Assert.IsNull(result.Error);
            Assert.IsTrue(result.Results.Count == count);
        }

        [Test]
        [TestCase("https://yandex.ru/search/xml")]
        public void ShouldReturnErrorResultIfInvalidSearch(string uri)
        {
            _options.Uri = uri;
            var searchEngine = new YandexSearchEngine(_options);
            var result = searchEngine.Search("nginx");

            Assert.IsNotNull(result.Error);
            Assert.That(result.Error.Title == "Error Code is 48");
            Assert.That(result.Error.Description.StartsWith("Неверный тип поиска"));

        }

        //This test must return positive if before get data from api you accept your IP-address on site https://xml.yandex.ru/settings/
        [Test]
        public void ShouldReturnErrorResultIfIpNotRegister()
        {            
            var searchEngine = new YandexSearchEngine(_options);
            var result = searchEngine.Search("nginx");

            Assert.IsNotNull(result.Error);
            Assert.That(result.Error.Title == "Error Code is 33");
            Assert.That(result.Error.Description.Contains("is not in this user's list of permitted IP addresses"));

        }

        [Test]
        [TestCase("https://yandex.ru/search/xml")]
        public async Task ShouldReturnErrorResultIfInvalidSearchAsync(string uri)
        {
            _options.Uri = uri;
            var searchEngine = new YandexSearchEngine(_options);
            var result = await searchEngine.SearchAsync("nginx");

            Assert.IsNotNull(result.Error);
            Assert.That(result.Error.Title == "Error Code is 48");
            Assert.That(result.Error.Description.StartsWith("Неверный тип поиска"));

        }

        [Test]
        [TestCase("03.304041461:62374326f8f0c193806a26f0cc0511be", "")]
        [TestCase("","anonymous33")]
        public void ShouldReturnErrorResultIfInvalidApiKeyOrUserName(string api, string username)
        {
            _options.Apikey = string.IsNullOrWhiteSpace(api) ? api : _options.Apikey;
            _options.Username = string.IsNullOrWhiteSpace(username) ? username : _options.Username;

            var searchEngine = new YandexSearchEngine(_options);
            var result = searchEngine.Search("nginx");

            Assert.IsNotNull(result.Error);
            Assert.That(result.Error.Title == "Error Code is 42");
            Assert.That(result.Error.Description.StartsWith("Invalid key"));
        }

        [Test]
        [TestCase("03.304041461:62374326f8f0c193806a26f0cc0511be", "")]
        [TestCase("", "anonymous33")]
        public async Task ShouldReturnErrorResultIfInvalidApiKeyOrUserNameAsync(string api, string username)
        {
            _options.Apikey = string.IsNullOrWhiteSpace(api) ? api : _options.Apikey;
            _options.Username = string.IsNullOrWhiteSpace(username) ? username : _options.Username;

            var searchEngine = new YandexSearchEngine(_options);
            var result = await searchEngine.SearchAsync("nginx");

            Assert.IsNotNull(result.Error);
            Assert.That(result.Error.Title == "Error Code is 42");
            Assert.That(result.Error.Description.StartsWith("Invalid key"));
        }


    }
}