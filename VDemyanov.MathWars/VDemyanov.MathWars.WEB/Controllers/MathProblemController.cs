using Markdig;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        IImageService _imageService;
        ITopicService _topicService;
        ApplicationDbContext _applicationDbContext;
        UserManager<IdentityUser> _userManager;
        public IConfiguration Configuration { get; }

        public MathProblemController(IMathProblemService mathProblemService,
                                    IDropboxService dropboxService,
                                    IImageService imageService,
                                    ITopicService topicService,
                                    ApplicationDbContext applicationDbContext,
                                    UserManager<IdentityUser> userManager,
                                    IConfiguration configuration)
        {
            _mathProblemService = mathProblemService;
            _imageService = imageService;
            _dropboxService = dropboxService;
            _topicService = topicService;
            _userManager = userManager;
            Configuration = configuration;
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
            CreateViewModel viewModel = new CreateViewModel(_topicService.GetTopicNames(), userId);
            return View("Create", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            Topic topic = _topicService.GetTopicByName(model.Topic);

            MathProblem createdMP = new MathProblem()
            {
                Name = model.Title,
                Summary = model.Summary,
                CreationDate = DateTime.Now,
                LastEditDate = DateTime.Now,
                User = await _userManager.FindByIdAsync(model.UserId),
                Topic = topic
            };

            _mathProblemService.Create(createdMP);

            foreach (IFormFile imageFile in Request.Form.Files)
            {
                string url = await _dropboxService.Upload("/public", imageFile.FileName, model.UserId, await imageFile.GetBytes());
                Image image = new Image() { Link = url, MathProblem = createdMP };
                //_imageService.Create(image);
            }

            return Json(new { status = true, Message = "MathProblem is created" });
        }
    }
}
