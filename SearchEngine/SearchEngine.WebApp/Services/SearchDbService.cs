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
            //TODO: DateTime in db must default get value
            //TODO: add DTO with searching word and searchresult
            //TODO: do we need ResultId
            var request = new Request { Date = DateTime.Now, SearchWord = word, Engine = searchresult.SearchTitle, 
                                        Results = new List<Result> 
                                        { 
                                            new Result { ItemsCount = searchresult.Results.Count, Items = searchresult.Results } } 
                                        };

            await _context.Requests.AddAsync(request);
            await _context.SaveChangesAsync();            
        }
    }
}
