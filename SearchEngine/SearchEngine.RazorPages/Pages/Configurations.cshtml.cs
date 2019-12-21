using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace SearchEngine.RazorPages
{
    public class ConfigurationsModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public string EngineName { get; set; }
        public IEnumerable<string> Sections { get; set; }
        
        public Dictionary<string,string> Fields { get; set; }

        public ConfigurationsModel(IConfiguration configuration)
        {
            _configuration = configuration;

            Sections = _configuration.GetSection("EnginesConfig").GetChildren().Select(x => x.Key);            
        }        

        public void OnGet(string selSection)
        {
            EngineName = selSection ?? Sections.First();
            FillConfigurationFields();
        }

        public void OnPost(Dictionary<string, string> dict)
        {            
            var engineSection = _configuration.GetSection("EnginesConfig").GetSection(EngineName);

            foreach (var pair in dict)
            {
                if (engineSection.GetSection(pair.Key).Exists())
                {
                    engineSection[pair.Key] = pair.Value;
                }
            }
        }

        private void FillConfigurationFields() => Fields = _configuration.GetSection("EnginesConfig")
                                                                                    .GetSection(EngineName)
                                                                                    .GetChildren()
                                                                                    .ToDictionary(x => x.Key, y => y.Value);
    }
}