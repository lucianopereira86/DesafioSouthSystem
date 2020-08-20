using DesafioSouthSystem.Shared.CommandQuery;
using MediatR;

namespace DesafioSouthSystem.Domain.Commands
{
    public class FileReadingCommand : IRequest<CommandQueryResult>
    {
        public FileReadingCommand(string name, string inputPath, string outputPath)
        {
            Name = name;
            InputPath = inputPath;
            OutputPath = outputPath;
        }

        public string Name { get; private set; }
        public string InputPath { get; private set; }
        public string OutputPath { get; private set; }
    }
}
