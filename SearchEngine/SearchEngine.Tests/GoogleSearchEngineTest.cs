using NUnit.Framework;
using SearchEngine.Core.Configurations;
using SearchEngine.Core.Engines;
using System.Threading.Tasks;

namespace SearchEngine.Test
{
    [TestFixture]
    public class GoogleSearchEngineTest
    {
        private SearchConfig config;
        private GoogleSearchOptions _options;
        private ISearchEngine _searchEngine;
        public GoogleSearchEngineTest()
        {
            config = new SearchConfig { ApiKey = "", AppId = "" };
            _options = new GoogleSearchOptions("google", "", "AIzaSyB3ex6PDKCy54J92_1rB0q1TBn1jbv43SU", "012320430393294220051:cxnkugrhcjf");
        }

        [SetUp]
        public void SetUp()
        {
            config = new SearchConfig { ApiKey = "", AppId = "" };
            _options = new GoogleSearchOptions("google", "", "AIzaSyB3ex6PDKCy54J92_1rB0q1TBn1jbv43SU", "012320430393294220051:cxnkugrhcjf");
            _searchEngine = new GoogleSearchEngine(_options);
        }
        //Expected: "Message[The provided API key is invalid.] Location[ - ] Reaso..."
        [Test]
        public void ShouldReturnSearchResult()
        {            
            var result = _searchEngine.Search("google");
                        
            Assert.IsNull(result.Error);
            Assert.IsTrue(result.Results.Count > 0);
            
        }

        [Test]
        public void ShouldThrowExceptionWhenApiIsWrong()
        {
            _options.Apikey = "AIzaSyB3fx6PDKCy54J92_1rB011TBn1jbv43SU";

            _searchEngine = new GoogleSearchEngine(_options);
            var result = _searchEngine.Search("yandex");

            Assert.That(result.Error != null);
            Assert.AreEqual(result.Error.Title, "BadRequest");
            Assert.That(result.Error.Description.StartsWith("Message[The provided API key is invalid.]"));
        }

        [Test]
        public void ShouldThrowExceptionWhenAppIdIsWrong()
        {
            //TODO: why error type changing
            _options.AppId = "hjkhj32423bkj5jkb3b5jT";

            _searchEngine = new GoogleSearchEngine(_options);
            var result = _searchEngine.Search("yandex");

            Assert.That(result.Error != null);
            Assert.AreEqual(result.Error.Title, "NotFound");
            Assert.That(result.Error.Description.StartsWith("Message[Requested entity was not found.]"));
        }

        [Test]
        public async Task ShouldReturnSearchResultAsync()
        {
            var result = await _searchEngine.SearchAsync("yandex");

            Assert.IsNull(result.Error);
            Assert.IsTrue(result.Results.Count >  0);
        }
    }
}
