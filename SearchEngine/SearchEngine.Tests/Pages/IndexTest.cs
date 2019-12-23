using NUnit.Framework;
using SearchEngine.Core.Services;
using SearchEngine.RazorPages.Pages;
using SearchEngine.RazorPages.Services;
using SearchEngine.Tests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.RazorPages.Test.Pages
{
    [TestFixture]
    public class IndexTest
    {
        private IndexModel _page;
        private IDatabaseService _dbService;
        private ISearchService _searchService;
        [SetUp]
        public void Setup()
        {
            _dbService = DefaultConfigs.DefaultISearchDbService;
            _searchService = DefaultConfigs.GetDefaultSearchService();
            _page = new IndexModel( _searchService, _dbService);
        }

        [Test]
        [TestCase("singleton")]
        public async Task ShouldCheckSearchResultOnPostAsync(string searchText)
        {
            await _page.OnPostAsync(searchText);
            Assert.IsNull(_page.SearchResult.Error);
        }
    }
}
