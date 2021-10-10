using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDemyanov.MathWars.DAL.Interfaces;
using VDemyanov.MathWars.DAL.Models;
using VDemyanov.MathWars.Service.Interfaces;

namespace VDemyanov.MathWars.Service.Implementation
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Tag> GetAll()
        {
            return _unitOfWork.Repository<Tag>().GetAll().ToList();
        }

        public void Create(Tag tag)
        {
            _unitOfWork.Repository<Tag>().Insert(tag);
            _unitOfWork.Save();
        }

        public void Create(List<Tag> tags)
        {
            foreach (var tag in tags)
                Create(tag);
        }

        public bool IsTagAlreadyAdded(string tagName)
        {
            Tag tag =  _unitOfWork.Repository<Tag>().GetFirstOrDefault(el => el.Name == tagName);
            if (tag is null)
                return false;
            else
                return true;
        }

        public void CreateTagsFromNames(List<string> tagNames)
        {
            foreach (string tagName in tagNames)
                if (!IsTagAlreadyAdded(tagName))
                    Create(new Tag() { Name = tagName });
        }

        public List<Tag> GetTagsByNames(List<string> tagNames)
        {
            return _unitOfWork.Repository<Tag>().Get(tag => tagNames.Contains(tag.Name)).ToList();
        }

        public List<string> GetNamesFromStr(string tagsStr)
        {
            return tagsStr.Split(",").Select(item=>item.Trim()).ToList();
        }
    }
}
