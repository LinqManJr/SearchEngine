using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using SearchEngine.Core.Configurations;

namespace SearchEngine.WebApp.Controllers
{
    public class ConfigurationController : Controller
    {
        private readonly IConfiguration _configuration;

        public ConfigurationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public IActionResult Index()
        {
            var sections = _configuration.GetSection("EnginesConfig").GetChildren().Select(x => x.Key).ToList();
            ViewBag.SectionNames = new SelectList(sections);
            return View();
        }

        public IActionResult GetFields(string sectionName)
        {
            var fields = _configuration.GetSection("EnginesConfig")
                                        .GetSection(sectionName)
                                        .GetChildren()
                                        .ToDictionary(x => x.Key, y => y.Value);
            ViewBag.SectionName = sectionName;

            return PartialView("_ConfigFields", fields);
        }
        [HttpPost]
        public IActionResult Save(Dictionary<string,string> dict)
        {
            var sectionName = dict.First().Value;
            var engineSection = _configuration.GetSection("EnginesConfig").GetSection(sectionName);

            foreach (var pair in dict.Skip(1))
            {
                if (engineSection.GetSection(pair.Key).Exists())
                {
                    engineSection[pair.Key] = pair.Value;
                }
            }
            return RedirectToAction("Index");
        }
    }
}