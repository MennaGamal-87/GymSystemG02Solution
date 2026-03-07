using GymSystemG02DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemG02DAL.Repositroies.Interfaces
{
    public interface IBookingRepository:IGenericRepository<MemberSession>
    {
        IEnumerable<MemberSession> GetSessionById(int sessionId);
    }
}
