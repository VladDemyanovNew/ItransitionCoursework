using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDemyanov.MathWars.DAL.Models;

namespace VDemyanov.MathWars.Service.Interfaces
{
    public interface IMathProblemService
    {
        void DeleteFromDb(int id);
        void Create(MathProblem mathProblem);
        Task<bool> Create(string tagsStr, string topicName, string title, string userId, string summary, IFormFileCollection images, List<string> answers);
        MathProblem GetById(int id);
        List<MathProblem> GetAllByUserId(string id);
        void Update(MathProblem mathProblem);
        Task<bool> Update(string tagsStr, string topicName, string title, string userId, string summary, int mpId, IFormFileCollection images, List<string> answers);
        List<MathProblem> GetAll();
        List<MathProblem> GetAllByTagName(string tag);
        List<MathProblem> GetAllByTopicName(string topicName);
        int CountMPCreatedByUser(string id);
        Task<List<MathProblem>> FullTextSearch(string text);
    }
}
