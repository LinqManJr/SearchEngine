using System;
using System.Threading.Tasks;

namespace SearchEngine.Core.Engines
{
    public interface ISearchEngine
    {
        SearchResult Search(string pattern);

        Task<SearchResult> SearchAsync(string pattern);
        
        //TODO: think about config if we have many Options
        //TODO: config builder to SearchEngines        
        
    }
}
