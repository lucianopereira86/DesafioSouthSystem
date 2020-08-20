using System.ComponentModel.DataAnnotations;

namespace DesafioSouthSystem.WebAPI.Models
{
    public class FileDownloadQueryVM
    {
        /// <summary>
        /// File name
        /// </summary>
        [Required]
        public string FileName { get; set; }
    }
}
