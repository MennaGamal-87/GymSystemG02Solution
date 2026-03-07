using GymSystemG02BLL.Services.Interfaces;
using GymSystemG02BLL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymSystemG02PL.Controllers
{
    [Authorize(Roles = "SuperAdmin")] // Only users with the "Admin" role can access this controller]
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        //ask ctor to inject the service layer for members
        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }//register for service in porgram

        #region Get All Members
        public IActionResult Index()
        {
            var members = _memberService.GetAllMembers();
            return View(members);
        }
        #endregion

        #region Get Member Details
        public ActionResult MemberDetails(int id)
        {
            if (id <= 0)
            {
                //Temp data Used to send data from Controller to view
                TempData["ErrorMessage"] = "ID Cannot Be 0 Or Negtive Number.";
                return RedirectToAction(nameof(Index));
            } //return it to get all members if the id is invalid
                
           var memberDetails = _memberService.GetMemberDetails(id);
            if (memberDetails == null) //if the member is not found, return to get all members
            {
                TempData["ErrorMessage"] = "Member Not Found ";
                return RedirectToAction(nameof(Index));
            } 
              return View(memberDetails);
        }
        #endregion

        #region Get Health Record Details 
        public ActionResult HealthRecordDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "ID Cannot Be 0 Or Negtive Number.";
                return RedirectToAction(nameof(Index));
            } //return it to get all members if the id is invalid
               
            var healthRecord = _memberService.GetMemberHealthRecordDetails(id);
            if (healthRecord == null)
            {
                TempData["ErrorMessage"] = "Health Record Not Found ";
                return RedirectToAction(nameof(Index));
            } //if the health record is not found, return to get all members
               
            return View(healthRecord);
        }
        #endregion

        #region Create Member
        public ActionResult Create()
        {
            return View();
        }

        //add to database so use httppost
        [HttpPost]
        public ActionResult CreateMember(CreateMemberViewModel CreatedMember)
        {
            //model state is used to check if the data entered by user is valid or not, it checks the data annotations in the view model
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Data Invalid", "Please Check Data And Missing Fields.");
                
                return View(nameof(Create), CreatedMember);//if have wrong data not delete what user already enter
            } //if the data is invalid, return to create view with the data entered by user
            bool Result = _memberService.CreateMembers(CreatedMember);
            if (Result)
            {
                TempData["SuccessMessage"] = "Member Created Successfully.";
                
            }
            else
            {
                TempData["ErrorMessage"] = "Failed To Create Member.";
               
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Edit Member
        //add view
        public ActionResult MemberEdit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "ID Cannot Be 0 Or Negtive Number.";
                return RedirectToAction(nameof(Index));
            } //return it to get all members if the id is invalid
            var memberToUpdate = _memberService.GetMemberToUpdate(id);
            if (memberToUpdate == null)
            {
                TempData["ErrorMessage"] = "Member Not Found ";
                return RedirectToAction(nameof(Index));
            } //if the member is not found, return to get all members
            return View(memberToUpdate);
        }
        [HttpPost]
        public ActionResult MemberEdit([FromRoute]int id ,MemberToUpdateViewModel memberToUpdate)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Data Invalid", "Please Check Data And Missing Fields.");
                return View(memberToUpdate);
            }
            var Result = _memberService.UpdateMemberDetails(id, memberToUpdate);   
            if (Result)
            {
                TempData["SuccessMessage"] = "Member Updated Successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed To Update Member.";
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
            var Member = _memberService.GetMemberDetails(id);
            if (Member == null)
            {
                TempData["ErrorMessage"] = "Member Not Found ";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.memberId = id;
            ViewBag.memberName = Member.Name;
            return View();
        }

        public IActionResult DeleteConfirmed([FromForm]int id)
        {
            var Result = _memberService.RemoveMember(id);
            if (Result)
            {
                TempData["SuccessMessage"] = "Member Deleted Successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed To Delete Member. Please Make Sure That Member Does Not Have Active Sessions.";
            }
            return RedirectToAction(nameof(Index));
        }


        #endregion
    }
}
