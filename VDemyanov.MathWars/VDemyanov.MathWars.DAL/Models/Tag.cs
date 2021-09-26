using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDemyanov.MathWars.DAL.Models.Base;

namespace VDemyanov.MathWars.DAL.Models
{
    public partial class Tag : Entity
    {
        public Tag()
        {
            MathProblemTags = new HashSet<MathProblemTag>();
        }

        public string Name { get; set; }

        public virtual ICollection<MathProblemTag> MathProblemTags { get; set; }
    }
}
