using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SearchEngine.Core.Models;
using SearchEngine.Core.Services;
using SearchEngine.RazorPages.Services;

namespace SearchEngine.RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ISearchService _searchService;
        private readonly IDatabaseService _dbService;
        
        public SearchResult SearchResult { get; set; }
        public IndexModel(ISearchService searchService, IDatabaseService dbService)
        {           
            _searchService = searchService;
            _dbService = dbService;
        }        

        public async Task OnPostAsync(string searchText)
        {
            if (searchText == null) return;
            SearchResult = await _searchService.SearchInManyAsync(searchText);

            if (SearchResult.Error == null)
                await _dbService.AddRequestToDb(SearchResult, searchText);
        }        
    }
}
