using BasketService.Models;
using FluentValidation;

namespace BasketService.Validators;

public class BasketValidator : AbstractValidator<BasketItem>
{
    public BasketValidator()
    {
        RuleFor(p => p.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0");
        RuleFor(p => p.LegoSetId).NotEmpty().WithMessage("LegoSetId is required");
    }
    
}