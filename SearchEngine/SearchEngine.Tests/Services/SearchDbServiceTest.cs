using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SearchEngine.Core.Models;
using SearchEngine.Domain.Context;
using SearchEngine.WebApp.Services;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngine.Tests.Services
{
    [TestFixture]
    public class SearchDbServiceTest
    {
        private SearchContext _context;
        private IDatabaseService _dbService;
        public SearchDbServiceTest()
        {           
        }

        [SetUp]
        public void Setup()
        {
            _context = new SearchContext(new DbContextOptionsBuilder<SearchContext>().UseInMemoryDatabase("SearchDb").Options);
            _dbService = new SearchDbService(_context);
        }
        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }


        private SearchResult DefaultSearchResult => new SearchResult
        {
            CountResult = 10000,
            SearchTitle = "word",
            Error = null,
            Results = DefaultConfigs.GetDefaultItemsOfResult
        };

        private async Task AddSearchResult(SearchResult result,int count)
        {
            foreach (var i in Enumerable.Range(0, count))
                await _dbService.AddRequestToDb(result, $"word{i}");
        }

        [Test]
        public async Task ShouldAddSearchResultToDataBase()
        {
            var word = "Some Word";
            var countBefore = await _context.Requests.CountAsync();
            var result = DefaultSearchResult;            

            await  _dbService.AddRequestToDb(result, word);

            Assert.That(countBefore < await _context.Requests.CountAsync());
        }

        [Test]
        public async Task ShouldReturnRequests()
        {
            var word = "Some Word";
            var result = DefaultSearchResult;
            var beforeCount = _context.Requests.Count();

            await AddSearchResult(result, 3);

            var requests = await _dbService.GetRequests();

            Assert.IsNotNull(requests);
            Assert.That(requests.Count() - beforeCount == 3);
        }

        [Test]
        public async Task ShouldReturnRequestByWord()
        {
            int count = 3;
            await AddSearchResult(DefaultSearchResult, count);
            await _dbService.AddRequestToDb(DefaultSearchResult, "new");

            var reqByWod = await _dbService.GetRequestsByWord("new");
            Assert.That(reqByWod.Count() == 1);
        }

        [Test]
        public async Task ShouldReturnWords()
        {
            await AddSearchResult(DefaultSearchResult, 3);
            await _dbService.AddRequestToDb(DefaultSearchResult, "new");
            await _dbService.AddRequestToDb(DefaultSearchResult, "old");
            await _dbService.AddRequestToDb(DefaultSearchResult, "old");
            var result = _dbService.GetWords().ToList();           

            Assert.That(result.Count(x => x.Word.StartsWith("word")) == 3);
            Assert.That(result.Count(x => x.Word == "new") == 1);
            Assert.That(result.Count(x => x.Word == "old") == 1);
        }

        [Test]
        public async Task ShouldReturnResultById()
        {
            await AddSearchResult(DefaultSearchResult, 3);
            var result = await _dbService.GetResultById(2);
            Assert.IsTrue(result.Id == 2);
        }

    }
}
