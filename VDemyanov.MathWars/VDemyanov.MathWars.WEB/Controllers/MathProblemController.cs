using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDemyanov.MathWars.Dal;
using VDemyanov.MathWars.DAL.Interfaces;
using VDemyanov.MathWars.DAL.Models;
using VDemyanov.MathWars.Service.Interfaces;

namespace VDemyanov.MathWars.WEB.Controllers
{
    public class MathProblemController : Controller
    {
        ApplicationDbContext _applicationDbContext;
        IMathProblemService _mathProblemService;

        public MathProblemController(ApplicationDbContext applicationDbContext, IMathProblemService mathProblemService)
        {
            _applicationDbContext = applicationDbContext;
            _mathProblemService = mathProblemService;
        }

        [HttpPost]
        public IActionResult Delete(string[] values)
        {
            if (values.Length != 0)
                foreach (string mpId in values)
                    _mathProblemService.DeleteFromDb(Convert.ToInt32(mpId));
            return RedirectToAction("Index", "Profile");
        }
    }
}
