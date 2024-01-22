using Microsoft.AspNetCore.Mvc;

namespace TestMVC.Controllers
{
    public class SettingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
