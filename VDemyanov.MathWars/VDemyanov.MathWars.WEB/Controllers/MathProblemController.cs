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
        ITagService _tagService;
        IMathProblemTagService _mathProblemTagService;
        IAnswerService _answerService;
        UserManager<IdentityUser> _userManager;
        public IConfiguration Configuration { get; }

        public MathProblemController(IMathProblemService mathProblemService,
                                    IDropboxService dropboxService,
                                    IImageService imageService,
                                    ITopicService topicService,
                                    ITagService tagService,
                                    IAnswerService answerService,
                                    IMathProblemTagService mathProblemTagService,
                                    UserManager<IdentityUser> userManager,
                                    IConfiguration configuration)
        {
            _mathProblemService = mathProblemService;
            _imageService = imageService;
            _dropboxService = dropboxService;
            _topicService = topicService;
            _tagService = tagService;
            _mathProblemTagService = mathProblemTagService;
            _answerService = answerService;
            _userManager = userManager;
            Configuration = configuration;
        }

        [HttpPost]
        public IActionResult Delete(string[] values, string userId)
        {
            if (values.Length != 0)
                foreach (string mpId in values)
                    _mathProblemService.DeleteFromDb(Convert.ToInt32(mpId));
            return RedirectToAction("Index", "Profile", new { userId = userId });
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
            _tagService.CreateTagsFromNames(_tagService.GetNamesFromStr(model.Tags));
            List<Tag> tags = _tagService.GetTagsByNames(_tagService.GetNamesFromStr(model.Tags));
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
            _mathProblemTagService.Create(_mathProblemTagService.GenerateMPT(createdMP, tags));
            await _imageService.Create(Request.Form.Files, createdMP, model.UserId);
            _answerService.Create(model.Answers, createdMP);

            return Json(new { status = true, Message = "MathProblem is created" });
        }

        [HttpGet]
        public IActionResult Update(string userId, string mpId)
        {
            MathProblem updatedMP = _mathProblemService.GetById(Convert.ToInt32(mpId));
            List<string> answrs = updatedMP.Answers.Select(answ => answ.AnswerText).ToList();

            if (answrs.Count < 3)
                answrs.AddRange(new String(' ', 3 - answrs.Count).Split());

            UpdateViewModel viewModel = new UpdateViewModel(
                _topicService.GetTopicNames(), 
                userId,
                updatedMP.Name,
                updatedMP.Topic.Name,
                String.Join(",",_tagService.GetTagsByMathProblemId(updatedMP.Id)),
                updatedMP.Summary,
                answrs,
                mpId
            );
            return View("Update", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {
            _tagService.CreateTagsFromNames(_tagService.GetNamesFromStr(model.Tags));
            List<Tag> tags = _tagService.GetTagsByNames(_tagService.GetNamesFromStr(model.Tags));
            Topic topic = _topicService.GetTopicByName(model.Topic);

            MathProblem updatedMP = _mathProblemService.GetById(Convert.ToInt32(model.MathProblemId));
            if (updatedMP != null)
            {
                updatedMP.Name = model.Title;
                updatedMP.Summary = model.Summary;
                updatedMP.LastEditDate = DateTime.Now;
                updatedMP.Topic = topic;
                updatedMP.TopicId = topic.Id;
                _mathProblemService.Update(updatedMP);

                _answerService.DeleteAllByMathProblemId(updatedMP.Id);
                _answerService.Create(model.Answers, updatedMP);

                _mathProblemTagService.DeleteAllByMathProblemId(updatedMP.Id);
                _mathProblemTagService.Create(_mathProblemTagService.GenerateMPT(updatedMP, tags));

                await _imageService.DeleteAllByMathProblemId(Convert.ToInt32(model.MathProblemId));
                await _imageService.Create(Request.Form.Files, updatedMP, model.UserId);

                return Json(new { status = true, Message = "MathProblem is updated" });
            } 
            else
            {
                return Json(new { status = false, Message = "MathProblem is not found " });
            } 
        }
    }
}
