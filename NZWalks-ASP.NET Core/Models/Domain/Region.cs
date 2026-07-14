using System.Collections.Specialized;

namespace NZWalks_ASP.NET_Core.Models.Domain
{
    public class Region
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }// it could have null values also 
    }
}
