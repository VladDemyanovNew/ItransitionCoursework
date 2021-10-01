using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDemyanov.MathWars.Dal;
using VDemyanov.MathWars.DAL.Interfaces;
using VDemyanov.MathWars.DAL.Models;
using VDemyanov.MathWars.DAL.UnitOfWork;
using VDemyanov.MathWars.WEB.Models;

namespace VDemyanov.MathWars.WEB.Controllers
{
    public class ProfileController : Controller
    {
        ApplicationDbContext _applicationDbContext;
        UserManager<IdentityUser> _userManager;
        IUnitOfWork _unitOfWork;

        public ProfileController(ApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public ActionResult Index()
        {
            //List<MathProblem> mathProblems = _unitOfWork.Repository<MathProblem>().GetAll().ToList();
            string currentUserId = _userManager.GetUserId(HttpContext.User);
            List<MathProblem> mathProblems = _unitOfWork.Repository<MathProblem>()
                                            .GetQuery((mp) => mp.UserId == currentUserId)
                                            .ToList();

            ProfileViewModel viewModel = new ProfileViewModel
            {
                MathProblems = mathProblems,
                Achievements = null
            };

            return View(viewModel);
        }

    }
}
