using NUnit.Framework;
using SearchEngine.Core.Configurations;
using SearchEngine.Core.Engines;
using System.Threading.Tasks;

namespace SearchEngine.Test
{
    [TestFixture]
    public class BingSearchEngineTest
    {
        private SearchConfig _config;
        private ISearchEngine _engine;
        [SetUp]
        public void SetUp()
        {
            _config = new SearchConfig { ApiKey = "", Url = "" };            
        }

        [Test]
        public void ShouldReturnResult()
        {
            _engine = new BingSearchEngine(_config);
            var result = _engine.Search("bing");

            Assert.IsNull(result.Error);
            Assert.That(result.Results.Count > 0);
        }

        [Test]
        public void ShouldReturnErrorResult()
        {
            _config.ApiKey = "2ef785999ce24554b5454343e32211";
            _engine = new BingSearchEngine(_config);
            var result = _engine.Search("bing");

            Assert.IsNotNull(result.Error);
            Assert.That(result.Error.Title == "ProtocolError");
        }

        [Test]
        public async Task ShouldReturnResultAsync()
        {
            _engine = new BingSearchEngine(_config);
            var result = await _engine.SearchAsync("bing");

            Assert.IsNull(result.Error);
            Assert.That(result.Results.Count > 0);
        }

        [Test]
        public async Task ShouldReturnErrorResultAsync()
        {
            _config.ApiKey = "2ef785999ce24554b5454343e32211";
            _engine = new BingSearchEngine(_config);
            var result = await _engine.SearchAsync("bing");

            Assert.IsNotNull(result.Error);
            Assert.That(result.Error.Title == "ProtocolError");
        }
    }
}
