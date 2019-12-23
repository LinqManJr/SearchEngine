using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SearchEngine.Domain.Models;
using SearchEngine.RazorPages.Services;

namespace SearchEngine.RazorPages
{
    public class DbSearchModel : PageModel
    {
        private readonly IDatabaseService _dbService;

        public List<string> DropdownWords { get; set; }        

        public IEnumerable<Request> Requests { get; set; }
        public DbSearchModel(IDatabaseService dbService)
        {
            _dbService = dbService;
            DropdownWords = _dbService.GetWords().ToList();
        }

        public async Task OnGetAsync()
        {            
            Requests = await _dbService.GetRequests();            
        }

        public async Task OnPostAsync(string filterWord)
        {            
            Requests = await _dbService.GetRequestsByWord(filterWord);            
        }

        public async Task<IActionResult> OnGetDetails(int id)
        {
            var result = await _dbService.GetResultById(id);            
            return Partial("../Partials/_Details", result);
        }
    }
}