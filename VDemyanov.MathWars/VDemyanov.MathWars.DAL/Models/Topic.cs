using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDemyanov.MathWars.DAL.Models.Base;

namespace VDemyanov.MathWars.DAL.Models
{
    public partial class Topic : Entity
    {
        public Topic()
        {
            MathProblems = new HashSet<MathProblem>();
        }

        public string Name { get; set; }

        public virtual ICollection<MathProblem> MathProblems { get; set; }
    }
}
