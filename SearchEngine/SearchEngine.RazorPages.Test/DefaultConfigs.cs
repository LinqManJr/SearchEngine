using Microsoft.EntityFrameworkCore;
using SearchEngine.Core.Configurations;
using SearchEngine.Core.Engines;
using SearchEngine.Core.Models;
using SearchEngine.Core.Services;
using SearchEngine.Domain.Context;
using SearchEngine.RazorPages.Services;
using System.Collections.Generic;

namespace SearchEngine.RazorPages.Test
{
    public static class DefaultConfigs
    {
        public static IList<ItemResult> GetDefaultItemsOfResult => new List<ItemResult>
        {
            new ItemResult("title1","link1"),
            new ItemResult("title2","link2"),
            new ItemResult("title3","link3"),
            new ItemResult("title4","link4"),
            new ItemResult("title5","link5"),
            new ItemResult("title6","link6"),
            new ItemResult("title7","link7"),
            new ItemResult("title8","link8"),
            new ItemResult("title9","link9"),
            new ItemResult("title10","link10")
        };

        public static ErrorItem GetDefaultErrorItem => new ErrorItem("BadRequest", "Something went wrong");

        public static ISearchService GetDefaultSearchService()
        {
            var googleOptions = new GoogleSearchOptions
            {
                Name = "google",
                Uri = string.Empty,
                Apikey = "",
                AppId = ""
            };

            return new SearchService(new List<ISearchEngine> { new GoogleSearchEngine(googleOptions) });
        }

        public static ISearchService GetBadSearchService()
        {
            var googleOptions = new GoogleSearchOptions
            {
                Name = "google",
                Uri = string.Empty,
                Apikey = "",
                AppId = ""
            };

            return new SearchService(new List<ISearchEngine> { new GoogleSearchEngine(googleOptions) });
        }

        public static IDatabaseService DefaultISearchDbService => new SearchDbService(DefaultContext);
        public static IDatabaseService GetDefaultServiceWithContext(SearchContext context)
        {
            return new SearchDbService(context);
        }
        public static SearchContext DefaultContext
        {
            get
            {
                return new SearchContext(new DbContextOptionsBuilder<SearchContext>().UseInMemoryDatabase("SearchDb").Options);
            }
        }

        public static SearchResult DefaultSearchResult => new SearchResult
        {
            CountResult = 10000,
            SearchTitle = "word",
            Error = null,
            Results = GetDefaultItemsOfResult
        };
    }
}
