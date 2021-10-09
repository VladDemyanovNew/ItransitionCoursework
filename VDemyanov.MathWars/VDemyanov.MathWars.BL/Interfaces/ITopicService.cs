using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDemyanov.MathWars.DAL.Models;

namespace VDemyanov.MathWars.Service.Interfaces
{
    public interface ITopicService
    {
        List<string> GetTopicNames();
        Topic GetTopicByName(string name);
    }
}
