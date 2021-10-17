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
        ITopicService _topicService;
        ITagService _tagService;
        IAnswerService _answerService;
        IAchievementsService _achievementsService;
        UserManager<IdentityUser> _userManager;
        public IConfiguration Configuration { get; }

        public MathProblemController(IMathProblemService mathProblemService, ITopicService topicService,
                                    ITagService tagService, IAnswerService answerService,
                                    IAchievementsService achievementsService, UserManager<IdentityUser> userManager,
                                    IConfiguration configuration)
        {
            _mathProblemService = mathProblemService;
            _topicService = topicService;
            _tagService = tagService;
            _answerService = answerService;
            _achievementsService = achievementsService;
            _userManager = userManager;
            Configuration = configuration;
        }

        [HttpPost]
        public IActionResult Delete(string[] values, string userId)
        {
            if (values.Length != 0)
                foreach (string mpId in values)
                {
                    //_imageService.DeleteAllByMathProblemId(Convert.ToInt32(mpId));
                    _mathProblemService.DeleteFromDb(Convert.ToInt32(mpId));
                }
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
            if (model.Answers.All(answ => string.IsNullOrWhiteSpace(answ)))
                ModelState.AddModelError("Answers", "Enter at least one answer");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isMpCreated = await _mathProblemService.Create(model.Tags, model.Topic, model.Title,
                                             model.UserId, model.Summary,
                                             Request.Form.Files, model.Answers);

            if (isMpCreated)
                return Json(new { status = true, Message = "MathProblem is created" });
            else
                return Json(new { status = false, Message = "MathProblem is not created" });
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
            if (model.Answers.All(answ => string.IsNullOrWhiteSpace(answ)))
                ModelState.AddModelError("Answers", "Enter at least one answer");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isMpUpdated = await _mathProblemService.Update(model.Tags, model.Topic, model.Title,
                                             model.UserId, model.Summary, Convert.ToInt32(model.MathProblemId),
                                             Request.Form.Files, model.Answers);

            if (isMpUpdated)
                return Json(new { status = true, Message = "MathProblem is updated" });
            else
                return Json(new { status = false, Message = "MathProblem is not found " });
        }

        [HttpGet]
        public IActionResult Show(string mpId)
        {
            MathProblem mp = _mathProblemService.GetById(Convert.ToInt32(mpId));
            ShowViewModel viewModel = new ShowViewModel()
            {
                MathProblem = mp
            };

            return View("Show", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Answer([FromBody]Answer answer)
        {
            if (_answerService.CheckAnswer(answer.AnswerText, answer.MathProblemId))
            {
                _achievementsService.Create(
                    _mathProblemService.GetById(answer.MathProblemId),
                    await _userManager.FindByIdAsync(_userManager.GetUserId(User))
                    );
                return Json(new { status = true, Message = $"Answer is recieved: {answer.MathProblemId} and {answer.AnswerText}" });
            }
            else
            {
                return Json(new { status = false, Message = $"Answer is recieved: {answer.MathProblemId} and {answer.AnswerText}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchText)
        {
            List<MathProblem> mathProblems;
            if (!string.IsNullOrEmpty(searchText))
                mathProblems = await _mathProblemService.FullTextSearch(searchText);
            else
                mathProblems = _mathProblemService.GetAll();

            SearchViewModel viewModel = new SearchViewModel()
            {
                MathProblems = mathProblems
            };

            return View("Search", viewModel);
        }
    }
}
