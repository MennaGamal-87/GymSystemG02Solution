using GymSystemG02BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymSystemG02PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnalyticsService _analyticsService;

        public HomeController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }
        public IActionResult Index()
        {
            var Data = _analyticsService.GetAnalyticsData();
               
            return View(Data);
        }
    }
}
