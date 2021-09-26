﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDemyanov.MathWars.DAL.Models.Base;

namespace VDemyanov.MathWars.DAL.Models
{
    public partial class Comment : Entity
    {
        public int? MathProblem { get; set; }
        public string UserId { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastEditDate { get; set; }
        public string Text { get; set; }

        public virtual MathProblem MathProblemNavigation { get; set; }
        public virtual IdentityUser UserNavigation { get; set; }
    }
}
