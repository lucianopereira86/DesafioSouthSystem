using DesafioSouthSystem.Shared.Models;
using Microsoft.Extensions.Options;

namespace DesafioSouthSystem.Shared.CommandQuery
{
    public class CommandQueryHandler
    {
        public readonly IOptions<AppSettings> _optionsAppSettings;
        public CommandQueryHandler(IOptions<AppSettings> optionsAppSettings)
        {
            _optionsAppSettings = optionsAppSettings;
        }
    }
}
