using Microsoft.EntityFrameworkCore;
using NZWalks_ASP.NET_Core.Data;
using NZWalks_ASP.NET_Core.Models.Domain;
using System.Runtime.CompilerServices;

namespace NZWalks_ASP.NET_Core.Repositories
{
    public class SqlRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;
      
        public SqlRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;

        }
        public async Task<List<Region>> GetAllAsync()
        {

            return await dbContext.Regions.ToListAsync();
        }
    }
}
