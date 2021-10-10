using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDemyanov.MathWars.Dal;
using VDemyanov.MathWars.DAL.Interfaces;
using VDemyanov.MathWars.WEB.Models;

namespace VDemyanov.MathWars.WEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext _applicationDbContext;
        UserManager<IdentityUser> _userManager;
        IUnitOfWork _unitOfWork;

        public AdminController(ApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            AdminViewModel viewModel = new AdminViewModel()
            {
                Users = _applicationDbContext.Users.ToList()
            };

            return View(viewModel);
        }
    }
}
