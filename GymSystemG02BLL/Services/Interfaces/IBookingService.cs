using GymSystemG02BLL.ViewModels.BookingViewModel;
using GymSystemG02BLL.ViewModels.MembershipsViewModel;
using GymSystemG02BLL.ViewModels.SessionsViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemG02BLL.Services.Interfaces
{
    public interface IBookingService
    {
        IEnumerable<SessionViewModel> GetAllSessionsWithTrainerAndCategory();
        IEnumerable<MemberForSessionViewModel> GetAllMembersForUpcomingSession(int id);
        IEnumerable<MemberForSessionViewModel> GetAllMembersForOngoingSession(int id);
        bool CreateBooking(CreateBookingViewModel createBookingViewModel);
        IEnumerable<MemberSelectListViewModel> GetMembersForDropdown(int id);
        bool MemberAttended(MemberAttendOrCancelViewModel model);
        bool CancelBooking(MemberAttendOrCancelViewModel model);
    }
}
