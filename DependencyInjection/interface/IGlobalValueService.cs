using TestMVC.Models;

namespace TestMVC.DependencyInjection.@interface
{
    public interface IGlobalValueService
    {
        ProductDTO.Root? GetMockupValue();
        void SetMockupValue(ProductDTO.Root value);
    }
}
