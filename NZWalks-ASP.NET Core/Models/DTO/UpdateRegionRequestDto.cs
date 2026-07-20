using System.ComponentModel.DataAnnotations;

namespace NZWalks_ASP.NET_Core.Models.DTO
{
    public class UpdateRegionRequestDto
    {

        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }// it could have null values also 
    }
}
