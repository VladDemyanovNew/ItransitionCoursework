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
    public class AnswerService : IAnswerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnswerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(List<string> answers, MathProblem mathProblem)
        {
            foreach (string answ in answers)
            {
                if (!string.IsNullOrEmpty(answ))
                    Create(new Answer() { MathProblem = mathProblem, AnswerText = answ });
            }
        }

        public void Create(Answer answer)
        {
            _unitOfWork.Repository<Answer>().Insert(answer);
            _unitOfWork.Save();
        }

        public List<Answer> GetByMathProblemId(int id)
        {
            return _unitOfWork.Repository<Answer>().Get(answ => answ.MathProblemId == id).ToList();
        }
    }
}
