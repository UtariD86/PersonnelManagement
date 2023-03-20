using Microsoft.AspNetCore.Mvc;

namespace PersonnelManagement.Mvc.Controllers
{
    public class ScheduleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
