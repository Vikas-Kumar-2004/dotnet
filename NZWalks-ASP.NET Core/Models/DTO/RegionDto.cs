namespace NZWalks_ASP.NET_Core.Models.DTO
{
    public class RegionDto
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }// it could have null values also 
    }
}
