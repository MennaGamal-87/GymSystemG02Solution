using GymSystemG02DAL.Data.Contexts;
using GymSystemG02DAL.Entities;
using GymSystemG02DAL.Repositroies.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemG02DAL.Repositroies.Classes
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymSystemDbContext _dbContext;

        public SessionRepository(GymSystemDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Session> GetAllSessionsWithTrainerAndCategory()
        {
            return _dbContext.Sessions
                .Include(s => s.SessionTrainer)
                .Include(s => s.SessionCategory)
                .ToList();
        }

        public int GetCountOfBookedSlots(int sessionId)
        {
            return _dbContext.MemberSessions
                .Count(bs => bs.SessionId == sessionId);
        }

        public Session? GetSessionWithTrainerAndCategory(int sessionId)
        {
            return _dbContext.Sessions
                .Include(s => s.SessionTrainer)
                .Include(s => s.SessionCategory)
                .FirstOrDefault(s => s.Id == sessionId);
        }
    }
}
