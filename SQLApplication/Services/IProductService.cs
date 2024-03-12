using SQLApplication.Models;

namespace SQLApplication.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
        //List<Product> GetProducts();
        Task<bool> IsBeta();
    }
}