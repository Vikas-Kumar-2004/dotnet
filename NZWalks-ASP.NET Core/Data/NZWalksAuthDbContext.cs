using Microsoft.EntityFrameworkCore;

namespace NZWalks_ASP.NET_Core.Data
{
    public class NZWalksAuthDbContext : DbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {


        }
    }
}
