using MySQLApp.Models;
using MySQLApp.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MySQLApp.Pages
{
    public class IndexModel : PageModel
    {
        public List<Product> Products;
        private readonly IProductService _productservice;

        public IndexModel(IProductService productService)
        {
            _productservice = productService;
        }

        public void OnGet()
        {
            Products = _productservice.GetProducts();
        }
    }
}
