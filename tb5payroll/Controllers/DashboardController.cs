using Microsoft.AspNetCore.Mvc;

namespace tb5payroll.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
