using FluentValidation;
using ProductsApp.DTO;

namespace ProductsApp.Validators
{
    public class ProductUpdateValidator : AbstractValidator<ProductUpdateDTO>
    {

        public ProductUpdateValidator() {
          
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Το πεδίο 'Όνομα' στη φόρμα δεν μπορεί να είναι κενό")
                .Length(2, 50)
                .WithMessage("Το πεδίο 'Όνομα' πρέπει να είναι μεταξ'υ 2 - 50 χαρακτήρες");

            RuleFor(p => p.Price)
                .NotEmpty()
                .WithMessage("Το πεδίο 'Τιμή' στη φόρμα δεν μπορεί να είναι κενό");
                
        }
        
    }
}
