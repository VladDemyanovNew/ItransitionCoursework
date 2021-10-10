using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDemyanov.MathWars.DAL.Interfaces;
using VDemyanov.MathWars.DAL.Models;
using VDemyanov.MathWars.Service.Interfaces;
using VDemyanov.MathWars.Service.Utils;

namespace VDemyanov.MathWars.Service.Implementation
{
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        IDropboxService _dropboxService;

        public ImageService(IUnitOfWork unitOfWork, IDropboxService dropboxService)
        {
            _unitOfWork = unitOfWork;
            _dropboxService = dropboxService;
        }
        public void Create(Image image)
        {
            _unitOfWork.Repository<Image>().Insert(image);
            _unitOfWork.Save();
        }

        public async Task Create(IFormFileCollection images, MathProblem mathProblem, string userId)
        {
            foreach (IFormFile imageFile in images)
            {
                string url = await _dropboxService.Upload("/public", imageFile.FileName, userId, await imageFile.GetBytes());
                Image image = new Image() { Link = url, MathProblem = mathProblem };
                Create(image);
            }
        }
    }
}
