using System.ComponentModel.DataAnnotations;

namespace NZWalks_ASP.NET_Core.Models.DTO
{
    public class ImageUploadRequestDto
    {
        [Required]
        public IFormFile? File { get; set; }

        public string FileName { get; set; } = string.Empty;

        public string? FileDescription { get; set; }

        
    }
}
