using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SQLApplication.Models;
using SQLApplication.Services;

namespace SQLApplication.Pages
{
    public class IndexModel : PageModel
    {
        public List<Product> Products;
        private readonly IProductService _productservice;
        public bool IsBeta;

        public IndexModel(IProductService productService) 
        {
            _productservice = productService;
        }

        

        public void OnGet()
        {
            // ProductService productsService = new ProductService();
            //Products=productservice.GetProducts();

            IsBeta = _productservice.IsBeta().Result; //result added as it is an async method
            Products = _productservice.GetProducts();
        }
    }
}
