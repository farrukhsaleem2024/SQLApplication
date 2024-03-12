using testApp.Models;

namespace testApp.Services
{
    public interface IProductService
    {
        //Task<List<Product>> GetProducts();
        List<Product> GetProducts();
       // Task<bool> IsBeta();
    }
}
