using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using SearchEngine.Core.Models;
using SearchEngine.Domain.Context;
using SearchEngine.Domain.Models;
using SearchEngine.WebApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
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
            _context = new SearchContext(new DbContextOptionsBuilder<SearchContext>().UseInMemoryDatabase("SearchDb").Options);
            _dbService = new SearchDbService(_context);
        }

        [Test]
        public async Task ShouldAddSearchResultToDataBase()
        {
            var word = "Some Word";
            var countBefore = await _context.Requests.CountAsync();
            var result = new SearchResult 
            {   
                CountResult = 10000, 
                SearchTitle = word, 
                Error = null, 
                Results = DefaultConfigs.GetDefaultItemsOfResult
            };

            await  _dbService.AddRequestToDb(result, word);

            Assert.That(countBefore < await _context.Requests.CountAsync());
        }
    }
}
