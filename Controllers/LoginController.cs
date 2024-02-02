using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using TestMVC.Models;

namespace TestMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoginController(ILogger<LoginController> logger,
        IWebHostEnvironment hostingEnvironment,
        IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View(new LoginDTO() { UserName = string.Empty, Password = string.Empty });
        }

        [HttpPost]
        public ActionResult Login(string UserName, string Password)
        {
            ViewBag.error = "Login failed Please Verify your username and password";

            string PathMockup = Path.Combine(_hostingEnvironment.ContentRootPath, "Mockup", "User");
            string PathJson = Path.Combine(PathMockup, "User.json");
            var JsonPath = JsonFileReader.ReadAsync<List<UsernameDTO>>(PathJson).Result;

            bool isCorrect = false;
            UsernameDTO obj = new UsernameDTO();

            // loop for
            for (int i = 0; i < JsonPath.Count; i++)
            {
                if (JsonPath[i].username == UserName && JsonPath[i].password == Password)
                {
                    obj = JsonPath[i];
                    isCorrect = true;
                    break;
                }
            }

            // loop foreach
            // foreach (var item in JsonPath)
            // {
            //     if(item.username == UserName && item.password == Password)
            //     {
            //         isCorrect = true;
            //         break;
            //     }
            // }

            //// linq
            // isCorrect = JsonPath.Any(x => x.username == UserName && x.password == Password);

            if (isCorrect)
            {
                _httpContextAccessor.HttpContext?.Session.SetString("UserName", obj?.username ?? string.Empty);
                _httpContextAccessor.HttpContext?.Session.SetString("UserID", obj?.id.ToString() ?? string.Empty);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Index");
            }

        }

        //Logout
        public ActionResult Logout()
        {
            _httpContextAccessor.HttpContext?.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
