using DesafioSouthSystem.Shared.CommandQuery;
using MediatR;

namespace DesafioSouthSystem.Domain.Queries
{
    public class FileDownloadQuery : IRequest<CommandQueryResult>
    {
        public FileDownloadQuery(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; private set; }
        public string FilePath { get; private set; }

        public void SetOutputPath(string outputPath)
        {
            FilePath = outputPath + FileName;
        }
    }
}
