using Microsoft.AspNetCore.Mvc;

namespace tb5payroll.Controllers
{
    public class home : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
    }
}
