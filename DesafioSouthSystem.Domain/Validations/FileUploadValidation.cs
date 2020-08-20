using DesafioSouthSystem.Domain.Commands;
using FluentValidation;

namespace DesafioSouthSystem.Domain.Validations
{
    public class FileUploadValidation : AbstractValidator<FileUploadCommand>
    {
        public FileUploadValidation()
        {
            RuleFor(x => x.File)
                .Cascade(CascadeMode.Stop)
                .Must(m => m.Length > 0)
                .WithErrorCode("1001");
        }
    }
}
