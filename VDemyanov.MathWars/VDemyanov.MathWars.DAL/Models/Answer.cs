using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDemyanov.MathWars.DAL.Models.Base;

namespace VDemyanov.MathWars.DAL.Models
{
    public partial class Answer : Entity
    {
        public string AnswerText { get; set; }
        public int? MathProblem { get; set; }

        public virtual MathProblem MathProblemNavigation { get; set; }
    }
}
