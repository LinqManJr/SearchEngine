using SearchEngine.Core.Engines;
using SearchEngine.Core.Models;
using SearchEngine.RazorPages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngine.Core.Services
{
    public class SearchService : ISearchService
    {
        private readonly IEnumerable<ISearchEngine> _engines;        

        public SearchService(IEnumerable<ISearchEngine> engines)
        {
            _engines = engines;
        }        

        public async Task<SearchResult> SearchInManyAsync(string pattern)
        {
            if (_engines == null)
                throw new Exception();

            var orderedTasks = _engines.Select(x => x.SearchAsync(pattern)).OrderByCompletion();

            foreach (var task in orderedTasks)
            {
                var result = await task;

                if (result.Error == null)
                    return result;
            }

            return await orderedTasks[0];
        }

        public async Task<SearchResult> SearchInManyAsync(string pattern, params ISearchEngine[] engines)
        {
            var tasks = engines.Select(x => x.SearchAsync(pattern)).OrderByCompletion();
            var orderedTasks = _engines.Select(x => x.SearchAsync(pattern)).OrderByCompletion();

            foreach (var task in orderedTasks)
            {
                var result = await task;

                if (result.Error == null)
                    return result;
            }

            return await orderedTasks[0];
        }
    }
}
