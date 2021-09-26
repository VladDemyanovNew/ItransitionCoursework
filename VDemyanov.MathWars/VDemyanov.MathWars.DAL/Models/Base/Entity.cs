using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDemyanov.MathWars.DAL.Interfaces;

namespace VDemyanov.MathWars.DAL.Models.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
