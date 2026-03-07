using GymSystemG02DAL.Entities;
using GymSystemG02DAL.Repositroies.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemG02DAL.Repositroies.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new();
        int SaveChanges();
        public ISessionRepository SessionRepository { get; }
        IMembershipRepository MembershipRepository { get; }
        IBookingRepository BookingRepository { get; }
    }
}
