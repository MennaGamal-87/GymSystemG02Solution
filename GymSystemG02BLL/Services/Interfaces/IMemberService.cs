using GymSystemG02BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemG02BLL.Services.Interfaces
{
    public interface IMemberService
    {
        IEnumerable<MemberViewModel> GetAllMembers();
        bool CreateMembers(CreateMemberViewModel createMember);
        MemberViewModel? GetMemberDetails(int MemberId);
        //get health record
        HealthViewModel? GetMemberHealthRecordDetails(int MemberId);

        //Update Member Data
        //Need 2 action one to get data and another to  Update data

        //apply update
        bool UpdateMemberDetails(int id, MemberToUpdateViewModel updatedMember);

        //get member to update view model
        MemberToUpdateViewModel? GetMemberToUpdate(int MemberId);

        //Delete Member
        bool RemoveMember(int MemberId);


    }
}
