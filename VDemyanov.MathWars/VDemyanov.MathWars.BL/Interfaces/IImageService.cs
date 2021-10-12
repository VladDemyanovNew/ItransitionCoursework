using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDemyanov.MathWars.DAL.Models;

namespace VDemyanov.MathWars.Service.Interfaces
{
    public interface IImageService
    {
        void Create(Image image);
        Task Create(IFormFileCollection images, MathProblem mathProblem, string userId);
        Task DeleteAllByMathProblemId(int id);
        void Delete(int id);
        List<Image> GetAllByMathProblemId(int id);
    }
}
