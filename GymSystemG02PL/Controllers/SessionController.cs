using GymSystemG02BLL.Services.Interfaces;
using GymSystemG02BLL.ViewModels.SessionsViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymSystemG02PL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        #region Get All Sessions
        public IActionResult Index()
        {
            var sessions = _sessionService.GetAllSessions();
            return View(sessions);
        }
        #endregion

        #region Get Session Details
        public IActionResult Details(int id)
        {
            if(id <= 0)
            {
               TempData["ErrorMessage"] = "Invalid session ID.";
                return RedirectToAction("Index");
            }
            var session = _sessionService.GetSessionById(id);
            if (session == null)
            {
                TempData["ErrorMessage"] = "Session not found.";
                return RedirectToAction("Index");
            }
            return View(session);
        }
        #endregion

        #region Create Session
        public IActionResult Create()
        {
            LoadDropDownforcatgory();
            LoadDropDownForTrainer();
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateSessionViewModel CreatedSession)
        {
            if (!ModelState.IsValid)
            {

                LoadDropDownforcatgory();
                LoadDropDownForTrainer();

                return View(CreatedSession);

            }
            var Result=_sessionService.CreateSession(CreatedSession);
            if (!Result)
            {
                TempData["ErrorMessage"] = "Failed To Create Session";

                LoadDropDownforcatgory();
                LoadDropDownForTrainer();
                return View(CreatedSession);
            }
            else
            {
                TempData["SuccessMessaga"] = "Session Created Successfully";

                LoadDropDownforcatgory();
                LoadDropDownForTrainer();
                return RedirectToAction(nameof(Index));

            }


        }
        #endregion

        #region Edit Session
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id";
                return RedirectToAction(nameof(Index));
            }
            var Sessions = _sessionService.GetSessionToUpdate(id);
            if (Sessions is null)
            {
                TempData["ErrorMessage"] = $"Session Not Found.";
                return RedirectToAction("Index");
            }
            LoadDropDownForTrainer();
            return View(Sessions);
        }
        [HttpPost]
        public ActionResult Edit([FromRoute] int id, UpdateSessionViewModel updatedSession)
        {
            if (!ModelState.IsValid)
            {
               LoadDropDownForTrainer();

                return View(updatedSession);
            }
            var Result = _sessionService.UpdateSession(updatedSession,id);
            if (Result)
            {


                TempData["SuccessMessage"] = "Session Updated Successfully.";

                return RedirectToAction("Index");
              
            }
            else
            {

                TempData["ErrorMessage"] = "Failed o Update session";
                LoadDropDownForTrainer();

                return View(updatedSession);

            }

        }
        #endregion

        #region Delete Session
        //Delete 
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Inavlid Session Id.";
                return RedirectToAction(nameof(Index));
            }
            var sessions = _sessionService.GetSessionById(id);
            if (sessions == null)
            {
                TempData["ErrorMessage"] = $"Session Not Found.";
                return RedirectToAction("Index");
            }
            //need to send id from controller to view to use it in form action
            ViewBag.SessionId = id;
            return View(sessions);
        }
        //Submit for table
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var Result = _sessionService.RemoveSession(id);
            if (Result)
            {
                TempData["SuccessMessage"] = "Session Delete successfully.";
                
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to Delete session";
                

            }
            return RedirectToAction("Index");

        }
        #endregion

        #region Helper Method

        private void LoadDropDownForTrainer()
        {
            var Trainers = _sessionService.GetTrainerForSession();

            ViewBag.Trainers = new SelectList(Trainers, "Id", "Name");

        }
        private void LoadDropDownforcatgory()
        {

            var Categories = _sessionService.GetCategoryForSession();

            ViewBag.Categories = new SelectList(Categories, "Id", "Name");
        } 
        #endregion
    }
}
