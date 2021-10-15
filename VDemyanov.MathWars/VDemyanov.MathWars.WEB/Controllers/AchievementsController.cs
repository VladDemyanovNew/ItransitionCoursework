using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDemyanov.MathWars.Service.Interfaces;

namespace VDemyanov.MathWars.WEB.Controllers
{
    public class AchievementsController : Controller
    {
        IAchievementsService _achievementsService;
        IMathProblemService _mathProblemService;
        public AchievementsController(IAchievementsService achievementsService, IMathProblemService mathProblemService)
        {
            _achievementsService = achievementsService;
            _mathProblemService = mathProblemService;
        }

        [HttpGet]
        public JsonResult Stat(string userId)
        {
            return Json(new
            {
                Solved = _achievementsService.CountMPSolvedByUser(userId),
                Created = _mathProblemService.CountMPCreatedByUser(userId)
            });
        }
    }
}
