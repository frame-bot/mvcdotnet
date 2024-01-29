using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestMVC.Models;

namespace TestMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {

            var getUserName = _httpContextAccessor.HttpContext?.Session.GetString("UserName");
            var getUserID = _httpContextAccessor.HttpContext?.Session.GetString("UserID");

            string Paths = Path.Combine(_hostingEnvironment.ContentRootPath, "Content", "images");

            var product = new ProduceModel
            {
                Id = 0,
                Name = "Sample Product",
                ImagePath = Paths + "/45841.jpg" // Path to the image
            };

            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // [HttpPost]
        // public ActionResult Login(LoginDTO viewModel)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return View();
        //     }

        //     if (viewModel.UserName == "admin" && viewModel.Password == "admin")
        //     {
        //         _globalValueService.SetUserName(viewModel.UserName);
        //         return View(new ProduceModel());
        //     }
        //     return View();
        // }
    }
}