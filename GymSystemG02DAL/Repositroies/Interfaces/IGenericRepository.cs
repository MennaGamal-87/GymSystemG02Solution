using GymSystemG02DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemG02DAL.Repositroies.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        TEntity? GetById(int id);
        //can send arrgument or not
        IEnumerable<TEntity> GetAll(Func<TEntity,bool>? condition=null);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
