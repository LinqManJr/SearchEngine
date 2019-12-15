using SearchEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngine.WebApp.Services
{
    public interface IDatabaseService
    {
        Task AddRequestToDb(SearchResult searchresult, string word);
    }
}
