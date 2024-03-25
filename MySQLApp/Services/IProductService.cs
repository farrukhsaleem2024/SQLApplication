using MySQLApp.Models;

namespace MySQLApp.Services
{
    public interface IProductService
    {
        //Task<List<Product>> GetProducts();
        List<Product> GetProducts();
        //Task<bool> IsBeta();
    }
}
