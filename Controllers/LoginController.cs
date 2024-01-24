using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using TestMVC.Models;

namespace TestMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View(new LoginDTO() { UserName = string.Empty, Password = string.Empty });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string UserName, string Password)
        {
            ViewBag.error = "Login failed";

            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            if (UserName == "admin" && Password == "admin")
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
