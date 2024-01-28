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
        public LoginController(ILogger<LoginController> logger, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View(new LoginDTO() { UserName = string.Empty, Password = string.Empty });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string UserName, string Password)
        {
            ViewBag.error = "Login failed Please Verify your username and password";

            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            string PathMockup = Path.Combine(_hostingEnvironment.ContentRootPath, "Mockup", "User");
            string PathJson = Path.Combine(PathMockup, "User.json");
            var JsonPath = JsonFileReader.ReadAsync<List<UsernameDTO>>(PathJson).Result;

            bool isCorrect = false;
            
            // loop for
            for (int i = 0; i < JsonPath.Count; i++)
            {
                if (JsonPath[i].username == UserName && JsonPath[i].password == Password)
                {
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
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Index");
            }

        }

        //  public ActionResult Index()
        // {
        //     if (Session["idUser"] != null)
        //     {
        //         return View();
        //     }
        //     else
        //     {
        //         return RedirectToAction("Login");
        //     }
        // }

        //GET: Register

        // public ActionResult Register()
        // {
        //     return View();
        // }

        // POST: Register
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public ActionResult Register(User _user)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         var check = _db.Users.FirstOrDefault(s => s.Email == _user.Email);
        //         if (check == null)
        //         {
        //             _user.Password = GetMD5(_user.Password);
        //             _db.Configuration.ValidateOnSaveEnabled = false;
        //             _db.Users.Add(_user);
        //             _db.SaveChanges();
        //             return RedirectToAction("Index");
        //         }
        //         else
        //         {
        //             ViewBag.error = "Email already exists";
        //             return View();
        //         }


        //     }
        //     return View();


        // }

        // public ActionResult Login()
        // {
        //     return View();
        // }


        // //Logout
        // public ActionResult Logout()
        // {
        //     Session.Clear();//remove session
        //     return RedirectToAction("Login");
        // }



        //create a string MD5
        // public static string GetMD5(string str)
        // {
        //     MD5 md5 = new MD5CryptoServiceProvider();
        //     byte[] fromData = Encoding.UTF8.GetBytes(str);
        //     byte[] targetData = md5.ComputeHash(fromData);
        //     string byte2String = null;

        //     for (int i = 0; i < targetData.Length; i++)
        //     {
        //         byte2String += targetData[i].ToString("x2");

        //     }
        //     return byte2String;
        // }

    }
}
