using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks_ASP.NET_Core.Models.Domain;

namespace NZWalks_ASP.NET_Core.Data
{
    public class NZWalksDbContext : DbContext// DbContext manages the database connection.

    {
        public NZWalksDbContext(DbContextOptions options) : base(options)
        {


        }
        public DbSet<Walk> Walks { get; set; } // DbSet<Walk> represents the Walks table.


        public DbSet<Difficulty> Difficulties { get; set; }// DbSet<Difficulty> represents the Difficulties table.

        public DbSet<Region> Regions { get; set; }



    }

}
