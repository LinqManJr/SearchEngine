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

            CorrelateOptions(sections, ref engines);            

            return engines;
        }
        private void CorrelateOptions(IEnumerable<IConfigurationSection> sections, ref List<ISearchEngine> engines)
        {            
            foreach (var section in sections)
            {
                switch (section.Key)
                {
                    case "Yandex":
                        engines.Add(new YandexSearchEngine(section.Get<YandexSearchOptions>()));
                        continue;
                    case "Google":
                        engines.Add(new GoogleSearchEngine(section.Get<GoogleSearchOptions>()));
                        continue;
                    case "Bing":
                        engines.Add(new BingSearchEngine(section.Get<SearchEngineOptions>()));
                        continue;
                    default: continue;
                }
            }
        }
    }
}
