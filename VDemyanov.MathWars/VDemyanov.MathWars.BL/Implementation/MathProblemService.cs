using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDemyanov.MathWars.Dal;
using VDemyanov.MathWars.DAL.Interfaces;
using VDemyanov.MathWars.DAL.Models;
using VDemyanov.MathWars.Service.Interfaces;
using Korzh.EasyQuery.Linq;
using Microsoft.EntityFrameworkCore;

namespace VDemyanov.MathWars.Service.Implementation
{
    public class MathProblemService : IMathProblemService
    {
        private readonly IUnitOfWork _unitOfWork;
        ITopicService _topicService;
        ITagService _tagService;
        IMathProblemTagService _mathProblemTagService;
        IAchievementsService _achievementsService;
        UserManager<IdentityUser> _userManager;
        IAnswerService _answerService;
        IImageService _imageService;
        ApplicationDbContext _applicationDbContext;

        public MathProblemService(IUnitOfWork unitOfWork, ITopicService topicService,
                                  IAnswerService answerService, IImageService imageService,
                                  ApplicationDbContext applicationDbContext, ITagService tagService,
                                  IMathProblemTagService mathProblemTagService,
                                  IAchievementsService achievementsService,
                                  UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _topicService = topicService;
            _answerService = answerService;
            _imageService = imageService;
            _applicationDbContext = applicationDbContext;
            _tagService = tagService;
            _mathProblemTagService = mathProblemTagService;
            _achievementsService = achievementsService;
            _userManager = userManager;
        }

        public void DeleteFromDb(int id)
        {
            _unitOfWork.Repository<MathProblem>().Delete(id);
            _unitOfWork.Save();
        }

        public void Create(MathProblem mathProblem)
        {
            _unitOfWork.Repository<MathProblem>().Insert(mathProblem);
            _unitOfWork.Save();
        }

        public MathProblem GetById(int id)
        {
            MathProblem mp = _unitOfWork.Repository<MathProblem>().GetFirstOrDefault(mp => mp.Id == id);
            mp.Topic = _topicService.GetTopicById(mp.TopicId);
            mp.Answers = _answerService.GetByMathProblemId(id);
            mp.Images = _imageService.GetAllByMathProblemId(mp.Id);
            return mp;
        }

        public void Update(MathProblem mathProblem)
        {
            _unitOfWork.Repository<MathProblem>().Update(mathProblem);
            _unitOfWork.Save();
        }

        public async Task<List<MathProblem>> GetAllByUserId(string id)
        {
            List<MathProblem> mathProblems = _unitOfWork.Repository<MathProblem>().GetQuery(mp => mp.UserId == id).ToList();
            return await ConfigMP(mathProblems);
        }

        public async Task<List<MathProblem>> GetAll()
        {
            List<MathProblem> mathProblems = _unitOfWork.Repository<MathProblem>().GetAll().ToList();

            return await ConfigMP(mathProblems);
        }

        public async Task<List<MathProblem>> GetAllByTagName(string tag)
        {
            List<MathProblem> mathProblems = (from mp in _applicationDbContext.MathProblems
                                              join mpt in _applicationDbContext.MathProblemTags on mp.Id equals mpt.MathProblemId
                                              join t in _applicationDbContext.Tags on mpt.TagId equals t.Id
                                              where t.Name == tag
                                              select mp).ToList();
                
            return await ConfigMP(mathProblems);
        }

        public int CountMPCreatedByUser(string id)
        {
            return _unitOfWork.Repository<MathProblem>().GetQuery(mp => mp.UserId == id).Count();
        }

        public async Task<List<MathProblem>> FullTextSearch(string text)
        {

            List<MathProblem> mathProblems = (from mp in _applicationDbContext.MathProblems
                                              join mpt in _applicationDbContext.MathProblemTags on mp.Id equals mpt.MathProblemId
                                              join tag in _applicationDbContext.Tags on mpt.TagId equals tag.Id
                                              join topic in _applicationDbContext.Topics on mp.TopicId equals topic.Id
                                              select new
                                              {
                                                  Name = mp.Name, Summary = mp.Summary, CreationDate = mp.CreationDate,
                                                  LastEditDate = mp.LastEditDate, Id = mp.Id, TopicName = topic.Name,
                                                  TagName = tag.Name, TopicId = mp.TopicId, UserId = mp.UserId
                                              }).FullTextSearchQuery(text).ToList().Select(item => new MathProblem()
                                              {
                                                  Name = item.Name, Summary = item.Summary, CreationDate = item.CreationDate,
                                                  LastEditDate = item.LastEditDate, Id = item.Id, TopicId = item.TopicId, UserId = item.UserId
                                              }).GroupBy(item => item.Id).Select(item => item.First()).ToList();

            return await ConfigMP(mathProblems);
        }

        public async Task<bool> Create(string tagsStr, string topicName, string title, string userId, string summary, IFormFileCollection images, List<string> answers)
        {

            using (var transaction = _applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    _tagService.CreateTagsFromNames(_tagService.GetNamesFromStr(tagsStr));
                    List<Tag> tags = _tagService.GetTagsByNames(_tagService.GetNamesFromStr(tagsStr));
                    Topic topic = _topicService.GetTopicByName(topicName);

                    MathProblem createdMP = new MathProblem()
                    {
                        Name = title,
                        Summary = summary,
                        CreationDate = DateTime.Now,
                        LastEditDate = DateTime.Now,
                        User = await _userManager.FindByIdAsync(userId),
                        Topic = topic
                    };

                    Create(createdMP);
                    _mathProblemTagService.Create(_mathProblemTagService.GenerateMPT(createdMP, tags));
                    await _imageService.Create(images, createdMP, userId);
                    _answerService.Create(answers, createdMP);

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> Update(string tagsStr, string topicName, string title, string userId, string summary, int mpId, IFormFileCollection images, List<string> answers)
        {
            using (var transaction = _applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    MathProblem updatedMP = GetById(mpId);
                    if (updatedMP != null)
                    {
                        _tagService.CreateTagsFromNames(_tagService.GetNamesFromStr(tagsStr));
                        List<Tag> tags = _tagService.GetTagsByNames(_tagService.GetNamesFromStr(tagsStr));
                        Topic topic = _topicService.GetTopicByName(topicName);

                        updatedMP.Name = title;
                        updatedMP.Summary = summary;
                        updatedMP.LastEditDate = DateTime.Now;
                        updatedMP.Topic = topic;
                        updatedMP.TopicId = topic.Id;
                        Update(updatedMP);

                        _answerService.DeleteAllByMathProblemId(updatedMP.Id);
                        _answerService.Create(answers, updatedMP);

                        _mathProblemTagService.DeleteAllByMathProblemId(updatedMP.Id);
                        _mathProblemTagService.Create(_mathProblemTagService.GenerateMPT(updatedMP, tags));

                        await _imageService.DeleteAllByMathProblemId(mpId);
                        await _imageService.Create(images, updatedMP, userId);
                    } 
                    else
                    {
                        transaction.Rollback();
                        return false;
                    }

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<List<MathProblem>> GetAllByTopicName(string topicName)
        {
            List<MathProblem> mathProblems = (from mp in _applicationDbContext.MathProblems
                                              join topic in _applicationDbContext.Topics on mp.TopicId equals topic.Id
                                              where topic.Name == topicName
                                              select mp).ToList();
            
            return await ConfigMP(mathProblems);
        }

        private async Task<List<MathProblem>> ConfigMP(List<MathProblem> mathProblems)
        {
            foreach (var mp in mathProblems)
            {
                mp.Topic = _topicService.GetTopicById(mp.TopicId);
                mp.User = await _userManager.FindByIdAsync(mp.UserId);
                mp.Images = _imageService.GetAllByMathProblemId(mp.Id);
            }
            return mathProblems;
        }
    }
}
