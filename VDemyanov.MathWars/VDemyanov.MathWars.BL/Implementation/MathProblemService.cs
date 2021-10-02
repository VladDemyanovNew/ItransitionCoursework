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

        public MathProblemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteFromDb(int id)
        {
            _unitOfWork.Repository<MathProblem>().Delete(id);
            _unitOfWork.Save();
        }
    }
}
