using Markdig;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDemyanov.MathWars.Dal;
using VDemyanov.MathWars.DAL.Interfaces;
using VDemyanov.MathWars.DAL.Models;
using VDemyanov.MathWars.Service.Interfaces;
using VDemyanov.MathWars.Service.Utils;
using VDemyanov.MathWars.WEB.Models;

namespace VDemyanov.MathWars.WEB.Controllers
{
    public class MathProblemController : Controller
    {
        IMathProblemService _mathProblemService;
        IDropboxService _dropboxService;
        ApplicationDbContext _applicationDbContext;
        public IConfiguration Configuration { get; }

        public MathProblemController(IMathProblemService mathProblemService,
                                    IDropboxService dropboxService,
                                    ApplicationDbContext applicationDbContext,
                                    IConfiguration configuration)
        {
            _mathProblemService = mathProblemService;
            Configuration = configuration;
            _dropboxService = dropboxService;
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public IActionResult Delete(string[] values)
        {
            if (values.Length != 0)
                foreach (string mpId in values)
                    _mathProblemService.DeleteFromDb(Convert.ToInt32(mpId));
            return RedirectToAction("Index", "Profile");
        }

        [HttpGet]
        public IActionResult Create(string userId)
        {
            CreateViewModel viewModel = new CreateViewModel()
            {
                UserId = userId
            };

            return View("Create", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            var result = Markdown.ToHtml(model.Summary);
            foreach (IFormFile imageFile in Request.Form.Files)
            {

                string url = await _dropboxService.Upload("/public", imageFile.FileName, model.UserId, await imageFile.GetBytes());
                Image image = new Image() { Link = url, MathProblemId = 2 };
                _applicationDbContext.Images.Add(image);
                _applicationDbContext.SaveChanges();
            }

            return Json(new { status = true, Message = "MathProblem is created" });
        }
    }
}
