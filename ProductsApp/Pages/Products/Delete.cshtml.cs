using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductsApp.Models;
using ProductsApp.Services;

namespace ProductsApp.Pages.Products
{
    public class DeleteModel : PageModel
    {
        public List<Error> ErrorArray { get; set; } = new();
        private readonly IProductService? _productService;

        public DeleteModel(IProductService? productService)
        {
            _productService = productService;
        }


        public void OnGet(int id)
        {
            try
            {
                Product? product = _productService?.DeleteProduct(id);
                Response.Redirect("/Products/getall");
            }
            catch (Exception e)
            {
                ErrorArray.Add(new Error("", e.Message, ""));
            }
        }
    }
}

