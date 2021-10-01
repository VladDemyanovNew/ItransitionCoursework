using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDemyanov.MathWars.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        void Save();
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
    }
}
