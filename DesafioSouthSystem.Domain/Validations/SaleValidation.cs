using DesafioSouthSystem.Domain.Entities;
using FluentValidation;

namespace DesafioSouthSystem.Domain.Validations
{
    public class SaleValidation : AbstractValidator<Sale>
    {
        public SaleValidation()
        {
            RuleFor(x => x.SaleID)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                .WithErrorCode("1010");

            RuleFor(x => x.SalesmanName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithErrorCode("1011");

            RuleForEach(x => x.SaleItems)
                .Cascade(CascadeMode.Stop)
                .SetValidator(new SaleItemValidation());
        }
    }
}
