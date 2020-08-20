using DesafioSouthSystem.Domain.Queries;
using FluentValidation;
using System.IO;

namespace DesafioSouthSystem.Domain.Validations
{
    public class FileDownloadValidation : AbstractValidator<FileDownloadQuery>
    {
        public FileDownloadValidation()
        {
            RuleFor(x => x.FileName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithErrorCode("1002");

            RuleFor(x => x.FilePath)
                .Cascade(CascadeMode.Stop)
                .Must(m => File.Exists(m))
                .WithErrorCode("1003");
        }
    }
}
