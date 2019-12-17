using NUnit.Framework;
using SearchEngine.Core.Configurations;
using SearchEngine.Core.Engines;
using System.Threading.Tasks;

namespace SearchEngine.Tests.Engines
{
    [TestFixture]
    public class BingSearchEngineTest
    {
        private SearchEngineOptions _options;
        private ISearchEngine _engine;
        [SetUp]
        public void SetUp()
        {
            _options = DefaultConfigs.BingOptions;
        }

        [Test]
        public void ShouldReturnResult()
        {
            _engine = new BingSearchEngine(_options);
            var result = _engine.Search("nginx");

            Assert.IsNull(result.Error);
            Assert.That(result.Results.Count > 0);
        }

        [Test]
        public void ShouldReturnErrorResult()
        {
            _options.Apikey = "2ef785999ce24554b5454343e32211";
            _engine = new BingSearchEngine(_options);
            var result = _engine.Search("nginx");

            Assert.IsNotNull(result.Error);
            Assert.That(result.Error.Title == "ProtocolError");
        }

        [Test]
        public async Task ShouldReturnResultAsync()
        {
            _engine = new BingSearchEngine(_options);
            var result = await _engine.SearchAsync("nginx");

            Assert.IsNull(result.Error);
            Assert.That(result.Results.Count > 0);
        }

        [Test]
        public async Task ShouldReturnErrorResultAsync()
        {
            _options.Apikey = "2ef785999ce24554b5454343e32211";
            _engine = new BingSearchEngine(_options);
            var result = await _engine.SearchAsync("nginx");

            Assert.IsNotNull(result.Error);
            Assert.That(result.Error.Title == "ProtocolError");
        }
    }
}
