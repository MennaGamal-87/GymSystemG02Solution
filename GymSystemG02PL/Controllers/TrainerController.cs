using GymSystemG02BLL.Services.Classes;
using GymSystemG02BLL.Services.Interfaces;
using GymSystemG02BLL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymSystemG02PL.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class TrainerController : Controller
    {
        private readonly ITrainerService _trainerService;

        //ask ctor to inject the service layer for members
        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }//register for service in porgram

        #region Get All Trainer
        public IActionResult Index()
        {
            var trainer = _trainerService.GetAllTrainers();
            return View(trainer);
        }
        #endregion


        #region Get Member Details
        public ActionResult TrainerDetails(int id)
        {
            if (id <= 0)
            {
                //Temp data Used to send data from Controller to view
                TempData["ErrorMessage"] = "ID Cannot Be 0 Or Negtive Number.";
                return RedirectToAction(nameof(Index));
            } //return it to get all members if the id is invalid

            var trainerDetails = _trainerService.GetTrainerDetails(id);
            if (trainerDetails == null) //if the member is not found, return to get all members
            {
                TempData["ErrorMessage"] = "Member Not Found ";
                return RedirectToAction(nameof(Index));
            }
            return View(trainerDetails);
        }
        #endregion

        #region Create Trainer
        public ActionResult Create()
        {
            return View();
        }

        //add to database so use httppost
        [HttpPost]
        public ActionResult CreateTrainer(CreateTrainerViewModel createdTrainer)
        {
            //model state is used to check if the data entered by user is valid or not, it checks the data annotations in the view model
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Data Invalid", "Please Check Data And Missing Fields.");

                return View(nameof(Create), createdTrainer);//if have wrong data not delete what user already enter
            } //if the data is invalid, return to create view with the data entered by user
            bool Result = _trainerService.CreateTrainer(createdTrainer);
            if (Result)
            {
                TempData["SuccessMessage"] = "Trainer Created Successfully.";

            }
            else
            {
                TempData["ErrorMessage"] = "Failed To Create Trainer.";

            }
            return RedirectToAction(nameof(Index));
        }
        #endregion


        #region Edit Trainer
        //add view
        public ActionResult TarinerEdit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "ID Cannot Be 0 Or Negtive Number.";
                return RedirectToAction(nameof(Index));
            } //return it to get all trainers if the id is invalid
            var trainerToUpdate = _trainerService.GetTrainerToUpdate(id);
            if (trainerToUpdate == null)
            {
                TempData["ErrorMessage"] = "Member Not Found ";
                return RedirectToAction(nameof(Index));
            } //if the trainer is not found, return to get all trainers
            return View(trainerToUpdate);
        }
        [HttpPost]
        public ActionResult TarinerEdit([FromRoute] int id, TrainerToUpdateViewModel trainerToUpdate)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Data Invalid", "Please Check Data And Missing Fields.");
                return View(trainerToUpdate);
            }
            var Result = _trainerService.UpdateTrainerDetails( trainerToUpdate,id);
            if (Result)
            {
                TempData["SuccessMessage"] = "Trainer Updated Successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed To Update Trainer.";
            }
            return RedirectToAction(nameof(Index));


        }
        #endregion


        #region Delete member
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "ID Cannot Be 0 Or Negtive Number.";
                return RedirectToAction(nameof(Index));
            } //return it to get all members if the id is invalid
            var Trainer = _trainerService.GetTrainerDetails(id);
            if (Trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found ";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.trainerId = id;
            ViewBag.trainerName = Trainer.Name;
            return View();
        }

        public IActionResult DeleteConfirmed([FromForm] int id)
        {
            var Result = _trainerService.RemoveTrainer(id);
            if (Result)
            {
                TempData["SuccessMessage"] = "Trainer Deleted Successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed To Delete Trainer. ";
            }
            return RedirectToAction(nameof(Index));
        }


        #endregion
    }
}
