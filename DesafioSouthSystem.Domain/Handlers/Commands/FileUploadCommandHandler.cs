using DesafioSouthSystem.Domain.Commands;
using DesafioSouthSystem.Domain.Validations;
using DesafioSouthSystem.Shared.CommandQuery;
using DesafioSouthSystem.Shared.Models;
using MediatR;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DesafioSouthSystem.Domain.Handlers.Commands
{
    public class FileUploadCommandHandler : CommandQueryHandler, IRequestHandler<FileUploadCommand, CommandQueryResult>
    {
        public FileUploadCommandHandler(IOptions<AppSettings> optionsAppSettings):base(optionsAppSettings)
        {
        }
        public async Task<CommandQueryResult> Handle(FileUploadCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandQueryResult(_optionsAppSettings);
            commandResult.Validate(request, new FileUploadValidation());
            if (commandResult.Valid)
            {
                string filePath = request.InputPath + request.File.FileName;
                if (File.Exists(filePath))
                    File.Delete(filePath);
                using var stream = File.Create(filePath);
                await request.File.CopyToAsync(stream);
            }
            return commandResult;
        }
    }
}
