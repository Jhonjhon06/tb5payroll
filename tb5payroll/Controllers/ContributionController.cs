using Microsoft.AspNetCore.Mvc;

namespace tb5payroll.Controllers
{
    public class ContributionController : Controller
    {
        public IActionResult Contribution()
        {
            return View();
        }
    }
}
