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
    public class MathProblemTagService : IMathProblemTagService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MathProblemTagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(MathProblemTag mptag)
        {
            _unitOfWork.Repository<MathProblemTag>().Insert(mptag);
            _unitOfWork.Save();
        }

        public void Create(List<MathProblemTag> mptags)
        {
            foreach (var mptag in mptags)
                Create(mptag);
        }

        public List<MathProblemTag> GenerateMPT(MathProblem mp, List<Tag> tags)
        {
            List<MathProblemTag> mathProblemTags = new List<MathProblemTag>();
            foreach (var tag in tags)
                mathProblemTags.Add(new MathProblemTag() { MathProblem = mp, Tag = tag });

            return mathProblemTags;
        }
    }
}
