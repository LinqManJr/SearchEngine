using SearchEngine.Core.Models;
using System.Threading.Tasks;

namespace SearchEngine.Core.Engines
{
    public interface ISearchEngine
    {        
        SearchResult Search(string pattern);

        Task<SearchResult> SearchAsync(string pattern);                    
        
    }
}
