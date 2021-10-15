using Microsoft.AspNetCore.Identity;
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
    public class AchievementsService : IAchievementsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AchievementsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int CountMPSolvedByUser(string id)
        {
            return _unitOfWork.Repository<Achievement>().GetQuery(ach => ach.UserId == id).Count();
        }

        public void Create(MathProblem mp, IdentityUser user)
        {
            _unitOfWork.Repository<Achievement>().Insert(new Achievement() { MathProblem = mp, User = user });
            _unitOfWork.Save();
        }

        public bool HasMathProblemBeenAchived(string userId, int mpId)
        {
            Achievement ach = _unitOfWork.Repository<Achievement>().GetFirstOrDefault(ach => ach.MathProblemId == mpId && ach.UserId == userId);
            return ach != null;
        }
    }
}
