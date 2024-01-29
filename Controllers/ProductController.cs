using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TestMVC.DependencyInjection.@interface;
using TestMVC.Models;
using static TestMVC.Models.ProductDTO;

namespace TestMVC.Controllers
{
    public class ProductController : Controller
    {
        IWebHostEnvironment _hostingEnvironment { get; set; }
        IGlobalValueService _global { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductController(IWebHostEnvironment hostingEnvironment, IGlobalValueService global, IHttpContextAccessor httpContextAccessor)
        {
            _hostingEnvironment = hostingEnvironment;
            _global = global;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {

            _httpContextAccessor.HttpContext?.Session.SetString("UserName", "admin");
            _httpContextAccessor.HttpContext?.Session.SetString("UserID", "admin");
            var JsonPath = getProductByJson();
            _global.SetMockupValue(JsonPath);
            return View(JsonPath);
        }
        public IActionResult Detail(int id)
        {
            var JsonFile = _global.GetMockupValue();
            var findDetail = new ProductDTO.Product();
            foreach (var item in JsonFile?.products ?? new List<ProductDTO.Product>())
            {
                if (item.id == id)
                {
                    findDetail = item;
                    break;
                }
            }

            var model = new ModelProduct()
            {
                currentImage = findDetail?.images?[0] ?? string.Empty,
                currentIndex = 0,
            };

            return View(new ProductDTO.ModelDetail()
            {
                modelProduct = model,
                products = findDetail
            });
        }

        public IActionResult EditProductAdmin()
        {
            var JsonPath = getProductByJson();
            _global.SetMockupValue(JsonPath);
            return View(JsonPath);
        }
        private ProductDTO.Root getProductByJson()
        {

            string PathMockup = Path.Combine(_hostingEnvironment.ContentRootPath, "Mockup", "Product");
            string PathJson = Path.Combine(PathMockup, "Product.json");
            var JsonPath = JsonFileReader.ReadAsync<ProductDTO.Root>(PathJson).Result;
            return JsonPath;
        }

        // [HttpPost]
        // public JsonResult setCurrentImageValue([FromBody] PostSetCurrentImageValue value)
        // {
        //     TempData["currentImage"] = value.image;
        //     return Json(value);
        // }
        // public string ConvertNumberToComma(string value)
        // {
        //     return value.Replace(",", ".");
        // }

    }
    public static class JsonFileReader
    {
        public static async Task<T> ReadAsync<T>(string filePath)
        {
            using FileStream stream = System.IO.File.OpenRead(filePath);
            return await JsonSerializer.DeserializeAsync<T>(stream);
        }
    }
}
