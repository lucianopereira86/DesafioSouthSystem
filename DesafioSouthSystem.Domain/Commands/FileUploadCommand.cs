using DesafioSouthSystem.Shared.CommandQuery;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DesafioSouthSystem.Domain.Commands
{
    public class FileUploadCommand: IRequest<CommandQueryResult>
    {
        public FileUploadCommand(IFormFile file)
        {
            File = file;
        }

        public IFormFile File { get; private set; }
        public string InputPath { get; private set; }

        public void SetInputPath(string inputPath)
        {
            InputPath = inputPath;
        }
    }
}
