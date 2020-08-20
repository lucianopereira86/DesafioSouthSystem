using System.Collections.Generic;

namespace DesafioSouthSystem.Shared.Models
{
    public class AppSettings
    {
        public Dictionary<string, string> Errors { get; set; }
        public bool IsHomePathComplete { get; set; }
    }
}
