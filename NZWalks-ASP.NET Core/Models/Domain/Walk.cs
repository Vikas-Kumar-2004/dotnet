namespace NZWalks_ASP.NET_Core.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }//  Foreign Key


        public Guid RegionId { get; set; } //  // A Walk always belongs to one Region



        // Navigation property 

        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }




    }
}
