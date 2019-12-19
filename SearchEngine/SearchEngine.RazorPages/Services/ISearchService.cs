﻿using SearchEngine.Core.Engines;
using SearchEngine.Core.Models;
using System.Threading.Tasks;

namespace SearchEngine.RazorPages.Services
{
    public interface ISearchService
    {
        public Task<SearchResult> SearchInManyAsync(string pattern);
        public Task<SearchResult> SearchInManyAsync(string pattern, params ISearchEngine[] engines);
    }
}
