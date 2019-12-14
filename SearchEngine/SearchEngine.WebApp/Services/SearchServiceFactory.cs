using Microsoft.Extensions.Configuration;
using SearchEngine.Core.Engines;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchEngine.WebApp.Services
{
    public class SearchServiceFactory
    {
        private readonly IConfiguration _configuration;

        public SearchServiceFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<ISearchEngine> GetSearchEngines()
        {
            var engines = new List<ISearchEngine>();

            return engines;
        }
    }
}
