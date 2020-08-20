using DesafioSouthSystem.Domain.Entities;
using FluentValidation;

namespace DesafioSouthSystem.Domain.Validations
{
    public class CustomerValidation: AbstractValidator<Customer>
    {
        public CustomerValidation()
        {
            RuleFor(x => x.CNPJ)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                .WithErrorCode("1007");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithErrorCode("1008");

            RuleFor(x => x.BusinessArea)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithErrorCode("1009");
        }
    }
}
