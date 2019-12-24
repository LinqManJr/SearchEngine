using SearchEngine.Core.Models;
using SearchEngine.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchEngine.RazorPages.Services
{
    public interface IDatabaseService
    {
        Task AddRequestToDb(SearchResult searchresult, string word);
        Task<IEnumerable<Request>> GetRequests();
        Task<IEnumerable<Request>> GetRequestsByWord(string word);
        Task<Result> GetResultById(int id);

        IEnumerable<string> GetWords();
    }
}
