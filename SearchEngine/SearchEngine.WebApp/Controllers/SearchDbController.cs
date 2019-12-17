﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SearchEngine.WebApp.Services;

namespace SearchEngine.WebApp.Controllers
{
    public class SearchDbController : Controller
    {
        private readonly IDatabaseService _dbService;

        public SearchDbController(IDatabaseService dbService)
        {
            _dbService = dbService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.WordsCollection = new SelectList(_dbService.GetWords(), "Id", "Word");
            var result = await _dbService.GetRequests();
            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var details = await _dbService.GetResultById(id);

            if(details != null)
                return PartialView("_DetailsModal", details);
            return NotFound();
        }        

        public async Task<IActionResult> FilterByWord(string word)
        {
            var requests = await _dbService.GetRequestsByWord(word);
            return PartialView("_ResultView", requests);
        }
    }
}