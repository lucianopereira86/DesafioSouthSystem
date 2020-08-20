using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DesafioSouthSystem.WebAPI.Models
{
    public class FileUploadCommandVM
    {
        /// <summary>
        /// File content
        /// </summary>
        [Required]
        public IFormFile File { get; set; }
    }
}
