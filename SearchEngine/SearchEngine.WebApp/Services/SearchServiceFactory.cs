using Microsoft.Extensions.Configuration;
using SearchEngine.Core.Configurations;
using SearchEngine.Core.Engines;
using System.Collections.Generic;
using System.Linq;

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
            var sections = _configuration.GetSection("EnginesConfig").GetChildren().ToList();

           // var ya = sections.First(x => x.Key == "Yandex").Get<YandexSearchOptions>();
            var google = sections.First(x => x.Key == "Google").Get<GoogleSearchOptions>();
            var bing = sections.First(x => x.Key == "Bing").Get<SearchEngineOptions>();

            engines.AddRange(new List<ISearchEngine> 
            { 
                //new YandexSearchEngine(ya),
                new GoogleSearchEngine(google),
                new BingSearchEngine(bing)
            });

            return engines;
        }
    }
}
