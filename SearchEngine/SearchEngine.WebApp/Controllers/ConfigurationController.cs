using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

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
    }
}