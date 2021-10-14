using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VDemyanov.MathWars.DAL.Models;
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

        public IActionResult Index(string tag)
        {
            List<MathProblem> mathProblems;
            if (string.IsNullOrEmpty(tag))
                mathProblems = _mathProblemService.GetAll().OrderByDescending(mp => mp.LastEditDate).ToList();
            else
                mathProblems = _mathProblemService.GetAllByTagName(tag).OrderByDescending(mp => mp.LastEditDate).ToList();

            HomeViewModel viewModel = new HomeViewModel
            {
                MathProblems = mathProblems
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
