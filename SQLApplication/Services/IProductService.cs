using SQLApplication.Models;

namespace SQLApplication.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Task<bool> IsBeta();
    }
}