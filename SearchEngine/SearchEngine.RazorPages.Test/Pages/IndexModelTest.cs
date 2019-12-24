using NUnit.Framework;
using SearchEngine.RazorPages.Pages;
using SearchEngine.RazorPages.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.RazorPages.Test.Pages
{
    [TestFixture]
    public class IndexModelTest
    {
        private IndexModel _page;
        private IDatabaseService _dbService;

        [SetUp]
        public void Setup()
        {
            _dbService = DefaultConfigs.DefaultISearchDbService;
        }

        [Test]
        [TestCase("singleton")]
        public async Task ShouldCheckValidSearchResultOnPostAsync(string searchText)
        {
            _page = new IndexModel(DefaultConfigs.GetDefaultSearchService(), _dbService);
            await _page.OnPostAsync(searchText);
            Assert.IsNull(_page.SearchResult.Error);
        }

        [Test]
        [TestCase("singleton")]
        public async Task ShouldCheckErrorSearchResultOnPostAsync(string searchText)
        {
            _page = new IndexModel(DefaultConfigs.GetBadSearchService(), _dbService);
            await _page.OnPostAsync(searchText);
            Assert.IsNotNull(_page.SearchResult.Error);
        }
    }
}
