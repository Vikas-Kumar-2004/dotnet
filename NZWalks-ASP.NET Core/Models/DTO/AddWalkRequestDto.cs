namespace NZWalks_ASP.NET_Core.Models.DTO
{
    public class AddWalkRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }//  Foreign Key


        public Guid RegionId { get; set; } //  // A Walk always belongs to one Region
    }
}
