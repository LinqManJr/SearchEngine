using Microsoft.EntityFrameworkCore;
using SearchEngine.Core.Models;
using SearchEngine.Domain.Context;
using SearchEngine.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngine.WebApp.Services
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
            
            //TODO: add DTO with searching word and searchresult
            
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

        public async Task<IEnumerable<Request>> GetRequestsByEngine(string engine)
        {
            return await _context.Requests.Where(x => x.Engine == engine).Include(r => r.Result).ToListAsync();
        }
    }
}
