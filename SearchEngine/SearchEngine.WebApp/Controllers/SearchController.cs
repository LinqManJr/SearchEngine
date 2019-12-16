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
        private readonly IDatabaseService _dbService;

        public SearchController(ILogger<SearchController> logger, ISearchService searchService, IDatabaseService dbService)
        {
            _logger = logger;
            _searchService = searchService;
            _dbService = dbService;
        }

        public IActionResult Index()
        {            
            return View();
        }        
        
        public async Task<IActionResult> Results(string word = "nginx")
        {
            var requestResult = await _searchService.SearchInManyAsync(word);
            await _dbService.AddRequestToDb(requestResult, word);

            return PartialView("_SearchPartial", requestResult);            
        }        
    }
}
