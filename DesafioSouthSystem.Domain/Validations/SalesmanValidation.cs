using DesafioSouthSystem.Domain.Entities;
using FluentValidation;

namespace DesafioSouthSystem.Domain.Validations
{
    public class SalesmanValidation : AbstractValidator<Salesman>
    {
        public SalesmanValidation()
        {
            RuleFor(x => x.CPF)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                .WithErrorCode("1004");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithErrorCode("1005");

            RuleFor(x => x.Salary)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                .WithErrorCode("1006");
        }
    }
}
