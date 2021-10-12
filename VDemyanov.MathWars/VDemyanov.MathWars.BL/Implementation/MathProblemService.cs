using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDemyanov.MathWars.DAL.Interfaces;
using VDemyanov.MathWars.DAL.Models;
using VDemyanov.MathWars.Service.Interfaces;

namespace VDemyanov.MathWars.Service.Implementation
{
    public class MathProblemService : IMathProblemService
    {
        private readonly IUnitOfWork _unitOfWork;
        ITopicService _topicService;
        IAnswerService _answerService;
        IImageService _imageService;

        public MathProblemService(IUnitOfWork unitOfWork, ITopicService topicService, IAnswerService answerService, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _topicService = topicService;
            _answerService = answerService;
            _imageService = imageService;
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

        public List<MathProblem> GetAllByUserId(string id)
        {
            return _unitOfWork.Repository<MathProblem>().GetQuery(mp => mp.UserId == id).ToList();
        }

        public List<MathProblem> GetAll()
        {
            List<MathProblem> mathProblems = _unitOfWork.Repository<MathProblem>().GetAll().ToList();
            foreach (var mp in mathProblems)
            {
                mp.Images = _imageService.GetAllByMathProblemId(mp.Id);
            }
            return mathProblems;
        }
    }
}
