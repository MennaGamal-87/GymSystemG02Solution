using GymSystemG02BLL.Services.Interfaces;
using GymSystemG02BLL.ViewModels;
using GymSystemG02DAL.Entities;
using GymSystemG02DAL.Repositroies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemG02BLL.Services.Classes
{
    public class AnalyticsService:IAnalyticsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnalyticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public AnalyticsViewModel GetAnalyticsData()
        {
            var Sessions = _unitOfWork.SessionRepository.GetAll();
            return new AnalyticsViewModel
            {
                ActiveMembers = _unitOfWork.GetRepository<Membership>().GetAll(X=>X.Status=="Active")
                .Count(),
                TotalMembers= _unitOfWork.GetRepository<Member>().GetAll().Count(),
                TotalTrainers= _unitOfWork.GetRepository<Trainer>().GetAll().Count(),
                UpComingSessions= Sessions.Count(s => s.StartDate > DateTime.Now),
                OngoingSessions= Sessions.Count(s => s.StartDate <= DateTime.Now && s.EndDate >= DateTime.Now),
                CompletedSessions= Sessions.Count(s => s.EndDate < DateTime.Now)
            };
        }
    }
}
