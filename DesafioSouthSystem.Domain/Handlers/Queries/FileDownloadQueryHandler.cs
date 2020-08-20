using DesafioSouthSystem.Domain.Commands;
using DesafioSouthSystem.Domain.Entities;
using DesafioSouthSystem.Domain.Queries;
using DesafioSouthSystem.Domain.Validations;
using DesafioSouthSystem.Shared;
using DesafioSouthSystem.Shared.CommandQuery;
using DesafioSouthSystem.Shared.Models;
using MediatR;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DesafioSouthSystem.Domain.Handlers.Queries
{
    public class FileDownloadQueryHandler : CommandQueryHandler, IRequestHandler<FileDownloadQuery, CommandQueryResult>
    {
        public FileDownloadQueryHandler(IOptions<AppSettings> optionsAppSettings):base(optionsAppSettings)
        {
        }
        public async Task<CommandQueryResult> Handle(FileDownloadQuery request, CancellationToken cancellationToken)
        {
            var commandResult = new CommandQueryResult(_optionsAppSettings);
            commandResult.Validate(request, new FileDownloadValidation());
            if (commandResult.Valid)
            {
                var file = await File.ReadAllBytesAsync(request.FilePath);
                commandResult.SetData(file);
            }
            return commandResult;
        }
    }
}
