﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SearchEngine.Domain.Context;
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
            ViewBag.WordsCollection = _dbService.GetWords();
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
    }
}