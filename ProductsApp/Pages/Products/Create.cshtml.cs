using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductsApp.DTO;
using ProductsApp.Models;
using ProductsApp.Services;

namespace ProductsApp.Pages.Products
{
    public class CreateModel : PageModel
    {
        public ProductInsertDTO? ProductInsertDTO { get; set; } = new();
        public List<Error>? ErrorArray { get; set; } = new();

        private readonly IProductService? _productService;
        private readonly IValidator<ProductInsertDTO>? _productInsertValidator;
       

        public CreateModel(IProductService? productService, IValidator<ProductInsertDTO>? productInsertValidator)
        {
            _productService = productService;
            _productInsertValidator = productInsertValidator;
           
        }

        public void OnGet()
        {
        }

        public void OnPost(ProductInsertDTO dto)
        {
            ProductInsertDTO = dto;

            var validationResult = _productInsertValidator!.Validate(dto);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ErrorArray!.Add(new Error(error.ErrorCode, error.ErrorMessage, error.PropertyName));
                }
                return;
            }
            try
            {
                Product? product = _productService!.InsertProduct(dto);
                Response.Redirect("/Products/getall");

            } catch (Exception e)
            {
                ErrorArray!.Add(new Error("", e.Message, ""));
            }
        }
    }
}
