﻿using NUnit.Framework;
using SearchEngine.Core.Configurations;
using SearchEngine.Core.Engines;
using SearchEngine.Tests;
using System.Threading.Tasks;

namespace SearchEngine.Test
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
