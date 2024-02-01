namespace TestMVC.Models
{
    public class ProductDTO
    {
        public class Product
        {
            public int id { get; set; }
            public string? title { get; set; }
            public string? description { get; set; }
            public int? price { get; set; }
            public double? discountPercentage { get; set; }
            public double? rating { get; set; }
            public int? stock { get; set; }
            public string? brand { get; set; }
            public string? category { get; set; }
            public string? thumbnail { get; set; }
            public List<string>? images { get; set; }
        }

        public class Root
        {
            public List<Product>? products { get; set; }
            public int total { get; set; }
            public int skip { get; set; }
            public int limit { get; set; }
        }
        public class ModelProduct
        {
            public string? currentImage { get; set; }
            public int currentIndex { get; set; }
        }

        public class ModelDetail
        {
            public ModelProduct? modelProduct { get; set; }
            public Product? products { get; set; }
        }

        public class PostSetCurrentImageValue
        {
            public string? image { get; set; }
        }
    }
}
