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
            var JsonProductValue = getProductByJson();
            _global.SetMockupValue(JsonProductValue);
            return View(JsonProductValue);
        }
        public IActionResult Detail(int id)
        {
            var JsonProductValue = _global.GetMockupValue();
            var findDetail = new ProductDTO.Product();
            foreach (var item in JsonProductValue?.products ?? new List<ProductDTO.Product>())
            {
                if (item.id == id)
                {
                    findDetail = item;
                    break;
                }
            }

            var model = new ModelProduct()
            {
                currentImage = string.Empty,
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
            return View();
        }


        [HttpGet]
        public JsonResult loadItems()
        {
            var getJson = getProductByJson();
            return Json(getJson);
        }

        [HttpPost]
        public IActionResult addItem([FromBody] Product value)
        {
            var JsonProductValue = getProductByJson();

            // find max id
            int newId = 0;
            foreach (var item in JsonProductValue?.products ?? new List<ProductDTO.Product>())
            {
                if (item.id > newId)
                {
                    newId = item.id;
                }
            }

            value.id = newId + 1;
            JsonProductValue?.products?.Add(value);

            // save to json
            string PathJson = getPathProductJson();
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var jsonString = JsonSerializer.Serialize(JsonProductValue, options);
            System.IO.File.WriteAllText(PathJson, jsonString);
            _global.SetMockupValue(JsonProductValue ?? new ProductDTO.Root());
            return Json(value);
        }

        [HttpPut]
        public IActionResult updateItem([FromBody] Product value)
        {
            var JsonProductValue = getProductByJson();

            // find value and update to json
            foreach (var item in JsonProductValue?.products ?? new List<ProductDTO.Product>())
            {
                if (item.id == value.id)
                {
                    item.title = value.title;
                    item.description = value.description;
                    item.price = value.price;
                    item.discountPercentage = value.discountPercentage;
                    item.rating = value.rating;
                    item.stock = value.stock;
                    item.brand = value.brand;
                    item.category = value.category;
                    item.thumbnail = value.thumbnail;
                    item.images = value.images;
                    break;
                }
            }

            // save to json
            string PathJson = getPathProductJson();
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var jsonString = JsonSerializer.Serialize(JsonProductValue, options);
            System.IO.File.WriteAllText(PathJson, jsonString);
            _global.SetMockupValue(JsonProductValue ?? new ProductDTO.Root());
            return Json(value);
        }

        [HttpDelete]
        public IActionResult deleteItem(int id)
        {
            var JsonProductValue = getProductByJson();

            // find value and update to json
            foreach (var item in JsonProductValue?.products ?? new List<ProductDTO.Product>())
            {
                if (item.id == id)
                {
                    JsonProductValue?.products?.Remove(item);
                    break;
                }
            }

            // save to json
            string PathJson = getPathProductJson();
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var jsonString = JsonSerializer.Serialize(JsonProductValue, options);
            System.IO.File.WriteAllText(PathJson, jsonString);
            _global.SetMockupValue(JsonProductValue ?? new ProductDTO.Root());
            return Json(id);
        }

        private ProductDTO.Root getProductByJson()
        {
            string PathJson = getPathProductJson();
            var JsonPath = JsonFileReader.ReadAsync<ProductDTO.Root>(PathJson).Result;
            return JsonPath;
        }

        private string getPathProductJson()
        {
            string PathMockup = Path.Combine(_hostingEnvironment.ContentRootPath, "Mockup", "Product");
            string PathJson = Path.Combine(PathMockup, "Product.json");
            return PathJson;
        }

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
