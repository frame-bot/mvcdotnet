using Microsoft.AspNetCore.Mvc;

namespace TestMVC.Controllers
{
    public class ChartsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
