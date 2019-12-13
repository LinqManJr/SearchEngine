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
        private ISearchEngine _searchEngine;
        public GoogleSearchEngineTest()
        {
            config = new SearchConfig { ApiKey = "AIzaSyB3ex6PDKCy54J92_1rB0q1TBn1jbv43SU", AppId = "012320430393294220051:cxnkugrhcjf" };            
        }

        [SetUp]
        public void SetUp()
        {
            config = new SearchConfig { ApiKey = "AIzaSyB3ex6PDKCy54J92_1rB0q1TBn1jbv43SU", AppId = "012320430393294220051:cxnkugrhcjf" };
            _searchEngine = new GoogleSearchEngine(config);
        }

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
            config.ApiKey = "hjkhj32423bkj5jkb3b5jT";

            _searchEngine = new GoogleSearchEngine(config);
            var result = _searchEngine.Search("yandex");

            Assert.That(result.Error != null);
            Assert.AreEqual(result.Error.Title, "BadRequest");
            Assert.AreEqual(result.Error.Description, "Message[Bad Request] Location[ - ] Reason[keyInvalid] Domain[usageLimits]");
        }

        [Test]
        public void ShouldThrowExceptionWhenAppIdIsWrong()
        {
            config.AppId = "hjkhj32423bkj5jkb3b5jT";

            _searchEngine = new GoogleSearchEngine(config);
            var result = _searchEngine.Search("yandex");

            Assert.That(result.Error != null);
            Assert.AreEqual(result.Error.Title, "BadRequest");
            Assert.AreEqual(result.Error.Description, "Message[Invalid Value] Location[ - ] Reason[invalid] Domain[global]");
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
