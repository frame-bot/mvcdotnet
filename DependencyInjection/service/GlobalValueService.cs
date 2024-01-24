using TestMVC.DependencyInjection.@interface;
using TestMVC.Models;

namespace TestMVC.DependencyInjection.service
{
    public class GlobalValueService : IGlobalValueService
    {

        private ProductDTO.Root? _mockup { get; set; }
        private string? _userName { get; set; }

        public ProductDTO.Root? GetMockupValue()
        {
            return _mockup;
        }

        public void SetMockupValue(ProductDTO.Root value)
        {
            _mockup = value;
        }

        public string? GetUserName()
        {
            return _userName;
        }

        public void SetUserName(string value)
        {
            _userName = value;
        }
    }
}
