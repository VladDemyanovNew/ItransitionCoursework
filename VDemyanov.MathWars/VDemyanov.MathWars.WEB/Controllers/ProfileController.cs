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
using VDemyanov.MathWars.Service.Interfaces;
using VDemyanov.MathWars.WEB.Models;

namespace VDemyanov.MathWars.WEB.Controllers
{
    public class ProfileController : Controller
    {
        ApplicationDbContext _applicationDbContext;
        UserManager<IdentityUser> _userManager;
        IMathProblemService _mathProblemService;
        IUnitOfWork _unitOfWork;

        public ProfileController(
            ApplicationDbContext applicationDbContext,
            UserManager<IdentityUser> userManager,
            IMathProblemService mathProblemService,
            IUnitOfWork unitOfWork)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mathProblemService = mathProblemService;
        }

        [Authorize]
        public ActionResult Index(string userId)
        {
            ProfileViewModel viewModel = new ProfileViewModel
            {
                MathProblems = _mathProblemService.GetAllByUserId(userId),
                Achievements = null,
                UserId = userId
            };

            return View(viewModel);
        }

    }
}
