using NUnit.Framework;
using SearchEngine.Core.Models;
using SearchEngine.RazorPages.Services;
using SearchEngine.Tests;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngine.RazorPages.Test.Pages
{
    [TestFixture]
    public class DbSearchModelTest
    {
        private DbSearchModel _page;
        private IDatabaseService _dbService;

        [SetUp]
        public async Task SetUp()
        {
            _dbService = DefaultConfigs.DefaultISearchDbService;
            await AddSearchResult(DefaultConfigs.DefaultSearchResult, 5);
        }

        private async Task AddSearchResult(SearchResult result, int count)
        {
            foreach (var i in Enumerable.Range(0, count))
                await _dbService.AddRequestToDb(result, $"word{i}");
        }

        [Test]
        public void ShouldReturnDropdownWords()
        {         
            _page = new DbSearchModel(_dbService);
            var words = _page.DropdownWords;
            Assert.IsTrue(words.Count > 0);;            
        }

        [Test]
        public async Task ShouldReturnRequestsOnGetAsync()
        {
            _page = new DbSearchModel(_dbService);
            await _page.OnGetAsync();
            Assert.IsNotNull(_page.Requests);
            Assert.IsTrue(_page.Requests.Count() > 0);
        }

        [Test]
        public async Task ShouldReturnFilterByWordRequests()
        {
            _page = new DbSearchModel(_dbService);
            await _page.OnPostAsync("word1");
            var result = _page.Requests;
            Assert.That(result.Count() == 1);
        }

        [Test]
        public async Task ShouldReturnZeroCountFilterByWordRequests()
        {
            _page = new DbSearchModel(_dbService);
            await _page.OnPostAsync("sometext");
            var result = _page.Requests;
            Assert.That(result.Count() == 0);
        }        
    }
}
