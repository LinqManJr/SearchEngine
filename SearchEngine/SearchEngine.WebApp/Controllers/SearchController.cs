using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SearchEngine.WebApp.Services;

namespace SearchEngine.WebApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;
        private readonly ISearchService _searchService;

        public SearchController(ILogger<SearchController> logger, ISearchService searchService)
        {
            _logger = logger;
            _searchService = searchService;
        }

        public IActionResult Index()
        {            
            return View();
        }        
        
        public async Task<IActionResult> Results(string word = "murana")
        {
            var result = await _searchService.SearchInManyAsync(word);
            return PartialView("_SearchPartial", result);            
        }        
    }
}
