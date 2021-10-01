﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDemyanov.MathWars.DAL.Models;

namespace VDemyanov.MathWars.WEB.Models
{
    public class ProfileViewModel
    {
        public List<MathProblem> MathProblems { get; set; }
        public List<Achievement> Achievements { get; set; }
    }
}
