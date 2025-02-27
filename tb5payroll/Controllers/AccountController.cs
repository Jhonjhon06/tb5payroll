using Microsoft.AspNetCore.Mvc;

namespace tb5payroll.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
