using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDemyanov.MathWars.DAL.Models;

namespace VDemyanov.MathWars.Service.Interfaces
{
    public interface IAnswerService
    {
        void Create(List<string> answers, MathProblem mathProblem);
        void Create(Answer answer);
        List<Answer> GetByMathProblemId(int id);
    }
}
