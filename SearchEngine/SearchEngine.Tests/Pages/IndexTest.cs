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
