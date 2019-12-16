using SearchEngine.Core.Models;
using SearchEngine.Domain.Models;
using SearchEngine.WebApp.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchEngine.WebApp.Services
{
    public interface IDatabaseService
    {
        Task AddRequestToDb(SearchResult searchresult, string word);
        Task<IEnumerable<Request>> GetRequests();
        Task<IEnumerable<Request>> GetRequestsByWord(string word);
        Task<IEnumerable<Request>> GetRequestsByEngine(string engine);
        Task<Result> GetResultById(int id);
        
        IEnumerable<SearchWordDto> GetWords();
    }
}
