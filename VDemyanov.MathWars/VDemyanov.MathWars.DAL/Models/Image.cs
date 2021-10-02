using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDemyanov.MathWars.DAL.Models.Base;

namespace VDemyanov.MathWars.DAL.Models
{
    public partial class Image : Entity
    {
        public string Link { get; set; }
        public int MathProblemId { get; set; }

        public virtual MathProblem MathProblem { get; set; }
    }
}
