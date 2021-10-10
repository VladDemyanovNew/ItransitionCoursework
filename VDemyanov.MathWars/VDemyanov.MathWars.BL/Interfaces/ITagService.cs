using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDemyanov.MathWars.DAL.Models;

namespace VDemyanov.MathWars.Service.Interfaces
{
    public interface ITagService
    {
        List<Tag> GetAll();
        void Create(Tag tag);
        void Create(List<Tag> tags);
        bool IsTagAlreadyAdded(string tagName);
        void CreateTagsFromNames(List<string> tagNames);
        List<Tag> GetTagsByNames(List<string> tagNames);
        List<string> GetNamesFromStr(string tagsStr);
        List<string> GetTagsByMathProblemId(int id);
    }
}
