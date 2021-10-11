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

        public MathProblemService(IUnitOfWork unitOfWork, ITopicService topicService, IAnswerService answerService)
        {
            _unitOfWork = unitOfWork;
            _topicService = topicService;
            _answerService = answerService;
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
            return mp;
        }

        public void Update(MathProblem mathProblem)
        {
            _unitOfWork.Repository<MathProblem>().Update(mathProblem);
            _unitOfWork.Save();
        }
    }
}
