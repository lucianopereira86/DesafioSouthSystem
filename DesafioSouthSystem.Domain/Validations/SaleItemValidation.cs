using DesafioSouthSystem.Domain.Entities;
using FluentValidation;

namespace DesafioSouthSystem.Domain.Validations
{
    public class SaleItemValidation : AbstractValidator<SaleItem>
    {
        public SaleItemValidation()
        {
            RuleFor(x => x.ItemID)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                .WithErrorCode("1012");

            RuleFor(x => x.Price)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                .WithErrorCode("1013");

            RuleFor(x => x.Quatity)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                .WithErrorCode("1014");
        }
    }
}
