using Microsoft.AspNetCore.Mvc;

namespace tb5payroll.Controllers
{
    public class Dashboard : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
