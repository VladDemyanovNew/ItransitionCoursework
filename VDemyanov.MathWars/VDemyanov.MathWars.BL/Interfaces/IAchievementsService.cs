﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDemyanov.MathWars.Service.Interfaces
{
    public interface IAchievementsService
    {
        bool HasMathProblemBeenAchived(string userId, int mpId);
    }
}
