using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDemyanov.MathWars.DAL.Models;

namespace VDemyanov.MathWars.Service.Interfaces
{
    public interface IMathProblemService
    {
        void DeleteFromDb(int id);
        void Create(MathProblem mathProblem);
    }
}
