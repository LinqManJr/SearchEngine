using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SearchEngine.WebApp.Controllers
{
    public class SearchDbController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}