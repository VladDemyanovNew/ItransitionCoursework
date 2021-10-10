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
    public class TopicService : ITopicService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TopicService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Topic GetTopicById(int id)
        {
            return _unitOfWork.Repository<Topic>().GetFirstOrDefault(topic => topic.Id == id);
        }

        public Topic GetTopicByName(string name)
        {
            return _unitOfWork.Repository<Topic>().GetFirstOrDefault(topic => topic.Name == name);
        }

        public List<string> GetTopicNames()
        {
            return _unitOfWork.Repository<Topic>().GetAll().Select(topic => topic.Name).Distinct().ToList();
        }
    }
}
