using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using testApp.Models;
using testApp.Services;

namespace testApp.Pages
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
            Products = _productservice.GetProducts();
        }
    }
}
