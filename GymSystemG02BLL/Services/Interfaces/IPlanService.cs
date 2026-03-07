using GymSystemG02BLL.ViewModels.PlanViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemG02BLL.Services.Interfaces
{
    public interface IPlanService
    {
        IEnumerable<PlanViewModel> GetAllPlans();
        PlanViewModel? GetPlanById(int id);
         UpdatePlanViewModel? GetPlanToUpdate(int PlanId);
        bool UpdatePlan(int PlanId, UpdatePlanViewModel updatedPlan);

        //Remove Plan By Toggle Status
        bool ToggleStatus(int PlanId);

    }
}
