using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VDemyanov.MathWars.Service.Interfaces;
using VDemyanov.MathWars.WEB.Models;

namespace VDemyanov.MathWars.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IMathProblemService _mathProblemService;

        public HomeController(ILogger<HomeController> logger, IMathProblemService mathProblemService)
        {
            _logger = logger;
            _mathProblemService = mathProblemService;
        }

        public IActionResult Index()
        {
            HomeViewModel viewModel = new HomeViewModel
            {
                MathProblems = _mathProblemService.GetAll().OrderByDescending(mp => mp.LastEditDate).ToList()
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
