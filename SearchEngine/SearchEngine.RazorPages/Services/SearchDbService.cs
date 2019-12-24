using Microsoft.EntityFrameworkCore;
using SearchEngine.Core.Models;
using SearchEngine.Domain.Context;
using SearchEngine.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngine.RazorPages.Services
{
    public class SearchDbService : IDatabaseService
    {
        private readonly SearchContext _context;

        public SearchDbService(SearchContext context)
        {
            _context = context;
        }
        public async Task AddRequestToDb(SearchResult searchresult, string word)
        {         
            var request = new Request
            {
                SearchWord = word,
                Engine = searchresult.SearchTitle,
                Result = new Result { ItemsCount = searchresult.Results.Count, Items = searchresult.Results }
            };

            await _context.Requests.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Request>> GetRequests()
        {
            return await _context.Requests.Include(r => r.Result).ToListAsync();
        }

        public async Task<IEnumerable<Request>> GetRequestsByWord(string word)
        {
            return await _context.Requests.Where(x => x.SearchWord == word).Include(r => r.Result).ToListAsync();
        }

        public async Task<Result> GetResultById(int id) => await _context.Results.FirstOrDefaultAsync(x => x.Id == id);

        public IEnumerable<string> GetWords()
        {
            return _context.Requests.Select(x => x.SearchWord).Distinct();
        }        
    }
}
