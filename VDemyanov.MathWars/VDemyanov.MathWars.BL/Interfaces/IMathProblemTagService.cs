using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDemyanov.MathWars.DAL.Models;

namespace VDemyanov.MathWars.Service.Interfaces
{
    public interface IMathProblemTagService
    {
        void Create(MathProblemTag mptag);
        void Create(List<MathProblemTag> mptags);
        List<MathProblemTag> GenerateMPT(MathProblem mp, List<Tag> tags);
        void DeleteAllByMathProblemId(int id);
    }
}
