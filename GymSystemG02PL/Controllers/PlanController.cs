using GymSystemG02BLL.Services.Interfaces;
using GymSystemG02BLL.ViewModels.PlanViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymSystemG02PL.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        #region Get All Plans
        public IActionResult Index()
        {
            var Plans = _planService.GetAllPlans();
            return View(Plans);
        }
        #endregion

        #region Get Plan Details
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"]="ID Can't Be Zero Or Negative Number";
                return RedirectToAction(nameof(Index));
            }

            var Plan = _planService.GetPlanById(id);
            if (Plan is null)
            {
                TempData["ErrorMessage"]="Plan Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(Plan);
        }
        #endregion

        #region Edit Plan
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"]="ID Can't Be Zero Or Negative Number";
                return RedirectToAction(nameof(Index));
            }
            var Plan = _planService.GetPlanToUpdate(id);
            if (Plan == null)
            {
                TempData["ErrorMessage"]="Plan Not Found Or Can't Be Updated";
                return RedirectToAction(nameof(Index));
            }
            return View(Plan);
        }

        [HttpPost]
        public ActionResult Edit([FromRoute] int id,UpdatePlanViewModel updatedPlan)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Wrong Data","Check Data Again !");
                return View(updatedPlan);
            }
            var Result= _planService.UpdatePlan(id, updatedPlan);
            if (!Result)
            {
                TempData["ErrorMessage"]="Failed To  Update Plan !";
                return RedirectToAction(nameof(Index));
            }
            TempData["SuccessMessage"]="Plan Updated Successfully !";
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Soft Delete Action (Toggle Delete)
        [HttpPost]
        public ActionResult Activate(int id)
        {
            var Result = _planService.ToggleStatus(id);
            if (Result)
            {
                TempData["SuccessMessage"]="Plan Status Updated Successfully !";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"]="Failed To Update Plan Status !";
                return RedirectToAction(nameof(Index));
            }
        }
        #endregion
    }
}
