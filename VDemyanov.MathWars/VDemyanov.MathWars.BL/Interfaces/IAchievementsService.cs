using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDemyanov.MathWars.DAL.Models;

namespace VDemyanov.MathWars.Service.Interfaces
{
    public interface IAchievementsService
    {
        bool HasMathProblemBeenAchived(string userId, int mpId);
        void Create(MathProblem mp, IdentityUser user);
    }
}
