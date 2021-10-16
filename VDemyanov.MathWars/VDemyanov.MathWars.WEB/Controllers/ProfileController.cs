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
        ITopicService _topicService;
        IUnitOfWork _unitOfWork;

        public ProfileController(
            ApplicationDbContext applicationDbContext,
            UserManager<IdentityUser> userManager,
            IMathProblemService mathProblemService,
            ITopicService topicService,
            IUnitOfWork unitOfWork)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mathProblemService = mathProblemService;
            _topicService = topicService;
        }

        [Authorize]
        public ActionResult Index(string userId, string topic, SortState sortOrder = SortState.IdAsc)
        {
            List<MathProblem> mathProblems = _mathProblemService.GetAllByUserId(userId);
            FilterViewModel filterViewModel = Filter(ref mathProblems, topic);
            SortViewModel sortViewModel = Sort(ref mathProblems, sortOrder);

            ProfileViewModel viewModel = new ProfileViewModel
            {
                MathProblems = mathProblems,
                Achievements = null,
                UserId = userId,
                FilterViewModel = filterViewModel,
                SortViewModel = sortViewModel
            };

            return View(viewModel);
        }

        private SortViewModel Sort(ref List<MathProblem> mathProblems, SortState sortOrder)
        {
            mathProblems = (sortOrder switch
            {
                SortState.IdDesc => mathProblems.OrderByDescending(s => s.Id),
                SortState.NameAsc => mathProblems.OrderBy(s => s.Name),
                SortState.NameDesc => mathProblems.OrderByDescending(s => s.Name),
                SortState.CreationDateAsc => mathProblems.OrderBy(s => s.CreationDate),
                SortState.CreationDateDesc => mathProblems.OrderByDescending(s => s.CreationDate),
                SortState.LastEditDateAsc => mathProblems.OrderBy(s => s.LastEditDate),
                SortState.LastEditDateDesc => mathProblems.OrderByDescending(s => s.LastEditDate),
                SortState.TopicAsc => mathProblems.OrderBy(s => s.Topic.Name),
                SortState.TopicDesc => mathProblems.OrderByDescending(s => s.Topic.Name),
                _ => mathProblems.OrderBy(s => s.Id),
            }).ToList();
            return new SortViewModel(sortOrder);
        }

        private FilterViewModel Filter(ref List<MathProblem> mathProblems, string topic)
        {
            if (!String.IsNullOrEmpty(topic) && topic != "All")
                mathProblems = _mathProblemService.GetAllByTopicName(topic);

            List<string> topics = _topicService.GetTopicNames();

            return new FilterViewModel(topics, topic);
        }

    }
}
