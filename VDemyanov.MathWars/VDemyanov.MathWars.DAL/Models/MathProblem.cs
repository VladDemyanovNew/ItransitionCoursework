﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDemyanov.MathWars.DAL.Models.Base;

namespace VDemyanov.MathWars.DAL.Models
{
    public partial class MathProblem : Entity
    {
        public MathProblem()
        {
            Answers = new HashSet<Answer>();
            Achievements = new HashSet<Achievement>();
            Images = new HashSet<Image>();
            Comments = new HashSet<Comment>();
            MathProblemTags = new HashSet<MathProblemTag>();
        }

        public string Name { get; set; }
        public string Summary { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastEditDate { get; set; }
        public int? Topic { get; set; }

        public virtual Topic TopicNavigation { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Achievement> Achievements { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<MathProblemTag> MathProblemTags { get; set; }
    }
}
