using Microsoft.AspNetCore.Mvc;

namespace TestMVC.Controllers
{
    public class TableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
