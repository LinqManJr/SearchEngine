using NUnit.Framework;
using SearchEngine.Core.Configurations;
using SearchEngine.Core.Engines;
using System.Threading.Tasks;

namespace SearchEngine.Core.Test.Engines
{
    [TestFixture]
    public class GoogleSearchEngineTest
    {
        private GoogleSearchOptions _options;
        private ISearchEngine _searchEngine;
        public GoogleSearchEngineTest()
        {
            _options = DefaultConfigs.GoogleOptions;
        }

        [SetUp]
        public void SetUp()
        {
            _options = DefaultConfigs.GoogleOptions;
            _searchEngine = new GoogleSearchEngine(_options);
        }

        [Test]
        public void ShouldReturnSearchResult()
        {
            var result = _searchEngine.Search("nginx");

            Assert.IsNull(result.Error);
            Assert.IsTrue(result.Results.Count > 0);
        }

        [Test]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(24)]
        public void ShouldReturnValidCountOfResultItems(int count)
        {
            _options.NumItems = count;
            _searchEngine = new GoogleSearchEngine(_options);
            var result = _searchEngine.Search("nginx");

            Assert.IsNull(result.Error);
            Assert.IsTrue(result.Results.Count == count);
        }

        [Test]
        [TestCase(0)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(24)]
        public async Task ShouldReturnValidCountOfResultItemsAsync(int count)
        {
            _options.NumItems = count;
            _searchEngine = new GoogleSearchEngine(_options);
            var result = await _searchEngine.SearchAsync("nginx");

            Assert.IsNull(result.Error);
            Assert.IsTrue(result.Results.Count == count);
        }

        [Test]
        public async Task ShouldReturnSearchResultAsync()
        {
            var result = await _searchEngine.SearchAsync("nginx");

            Assert.IsNull(result.Error);
            Assert.IsTrue(result.Results.Count > 0);
        }

        [Test]
        [TestCase("AIzaSyB3fx6PDKCy54J92_1rB011TBn1jbv43SU")]
        public void ShouldThrowExceptionWhenApiIsWrong(string api)
        {
            _options.Apikey = api;

            _searchEngine = new GoogleSearchEngine(_options);
            var result = _searchEngine.Search("nginx");

            Assert.That(result.Error != null);
            Assert.AreEqual(result.Error.Title, "BadRequest");
            var desc = result.Error.Description;

            Assert.That(desc.StartsWith("Message[The provided API key is invalid.]") ||
                        desc.StartsWith("Message[Bad Request] Location[ - ] Reason[keyInvalid]"));
        }

        [Test]
        [TestCase("AIzaSyB3fx6PDKCy54J92_1rB011TBn1jbv43SU")]
        public async Task ShouldThrowExceptionWhenApiIsWrongAsync(string api)
        {
            _options.Apikey = api;

            _searchEngine = new GoogleSearchEngine(_options);
            var result = await _searchEngine.SearchAsync("nginx");

            Assert.That(result.Error != null);
            Assert.AreEqual(result.Error.Title, "BadRequest");
            var desc = result.Error.Description;

            Assert.That(desc.StartsWith("Message[The provided API key is invalid.]") ||
                        desc.StartsWith("Message[Bad Request] Location[ - ] Reason[keyInvalid]"));
        }

        [Test]
        [TestCase("hjkhj32423bkj5jkb3b5jT")]
        public void ShouldThrowExceptionWhenAppIdIsWrong(string appid)
        {
            _options.AppId = appid;

            _searchEngine = new GoogleSearchEngine(_options);
            var result = _searchEngine.Search("nginx");

            Assert.That(result.Error != null);
            Assert.That(result.Error.Title == "NotFound" || result.Error.Title == "BadRequest" || result.Error.Title == "NotSupportedError");
            Assert.That(result.Error.Description.StartsWith("Message[Requested entity was not found.]"));
        }

        [Test]
        [TestCase("hjkhj32423bkj5jkb3b5jT")]
        public async Task ShouldThrowExceptionWhenAppIdIsWrongAsync(string appid)
        {
            _options.AppId = appid;

            _searchEngine = new GoogleSearchEngine(_options);
            var result = await _searchEngine.SearchAsync("nginx");

            Assert.That(result.Error != null);
            Assert.That(result.Error.Title == "NotFound" || result.Error.Title == "BadRequest");
            Assert.That(result.Error.Description.StartsWith("Message[Requested entity was not found.]") ||
                        result.Error.Description.StartsWith("Message[Invalid Value]"));
        }


    }
}
