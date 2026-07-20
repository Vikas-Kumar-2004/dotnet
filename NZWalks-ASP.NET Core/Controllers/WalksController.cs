using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks_ASP.NET_Core.Data;
using NZWalks_ASP.NET_Core.Models.Domain;
using NZWalks_ASP.NET_Core.Models.DTO;

namespace NZWalks_ASP.NET_Core.Controllers
{
    // url:/api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        public WalksController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //---------------------------------------------------------------------------------------------------
        // POST: Create Walk

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            // Map DTO to Domain Model
            var walkDomainModel = new Walk
            {
                Name = addWalkRequestDto.Name,
                Description = addWalkRequestDto.Description,
                LengthInKm = addWalkRequestDto.LengthInKm,
                WalkImageUrl = addWalkRequestDto.WalkImageUrl,
                DifficultyId = addWalkRequestDto.DifficultyId,
                RegionId = addWalkRequestDto.RegionId
            };

            // Add the entity to the database
            await dbContext.Walks.AddAsync(walkDomainModel);

            // Save changes
            await dbContext.SaveChangesAsync();

            // Map Domain Model back to DTO
            var walkDto = new WalkDto
            {
                Id = walkDomainModel.Id,
                Name = walkDomainModel.Name,
                Description = walkDomainModel.Description,
                LengthInKm = walkDomainModel.LengthInKm,
                WalkImageUrl = walkDomainModel.WalkImageUrl,
                DifficultyId = walkDomainModel.DifficultyId,
                RegionId = walkDomainModel.RegionId
            };

            // Return HTTP 201 Created
            return CreatedAtAction(
                nameof(GetById),
                new { id = walkDto.Id },
                walkDto);
        }

        //--------------------------------------------------------------------------------------------------------
        // GET: GET Walk BY ID 
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await dbContext.Walks.FindAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            var walkDto = new WalkDto
            {
                Id = walkDomainModel.Id,
                Name = walkDomainModel.Name,
                Description = walkDomainModel.Description,
                LengthInKm = walkDomainModel.LengthInKm,
                WalkImageUrl = walkDomainModel.WalkImageUrl,
                DifficultyId = walkDomainModel.DifficultyId,
                RegionId = walkDomainModel.RegionId
            };

            return Ok(walkDto);
        }


        //-------------------------------------------------------------------------------------------------------------
        // GET: GET ALL Walks
        // :/api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walksQuery = dbContext.Walks
                .Include(x => x.Difficulty)
                .Include(x => x.Region)
                .AsQueryable();

            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walksQuery = walksQuery.Where(x => x.Name.Contains(filterQuery));
                }
            }
            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walksQuery = isAscending ?? true ? walksQuery.OrderBy(x => x.Name) : walksQuery.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase) || sortBy.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase))
                {
                    walksQuery = isAscending ?? true ? walksQuery.OrderBy(x => x.LengthInKm) : walksQuery.OrderByDescending(x => x.LengthInKm);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;
            walksQuery = walksQuery.Skip(skipResults).Take(pageSize);


            // Execute the query
            var walksDomain = await walksQuery.ToListAsync();


            var walksDto = new List<WalkDto>();

            foreach (var walk in walksDomain)
            {
                walksDto.Add(new WalkDto
                {
                    Id = walk.Id,
                    Name = walk.Name,
                    Description = walk.Description,
                    LengthInKm = walk.LengthInKm,
                    WalkImageUrl = walk.WalkImageUrl,
                    DifficultyId = walk.DifficultyId,
                    RegionId = walk.RegionId,
                    Region = new RegionDto
                    {
                        Id = walk.Region.Id,
                        Code = walk.Region.Code,
                        Name = walk.Region.Name,
                        RegionImageUrl = walk.Region.RegionImageUrl
                    },
                    Difficulty = new DifficultyDto
                    {
                        Id = walk.Difficulty.Id,
                        Name = walk.Difficulty.Name
                    }
                });
            }

            return Ok(walksDto);
        }

        // ---------------------------------------------------------------------------------------------------------------
        // UPDATE: UPDATE Walk
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update(
    [FromRoute] Guid id,
    [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            // Check if the walk exists
            var walkDomainModel = await dbContext.Walks
                .FirstOrDefaultAsync(x => x.Id == id);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            // Map DTO to Domain Model
            walkDomainModel.Name = updateWalkRequestDto.Name;
            walkDomainModel.Description = updateWalkRequestDto.Description;
            walkDomainModel.LengthInKm = updateWalkRequestDto.LengthInKm;
            walkDomainModel.WalkImageUrl = updateWalkRequestDto.WalkImageUrl;
            walkDomainModel.DifficultyId = updateWalkRequestDto.DifficultyId;
            walkDomainModel.RegionId = updateWalkRequestDto.RegionId;

            // Save changes
            await dbContext.SaveChangesAsync();

            // Map Domain Model to DTO
            var walkDto = new WalkDto
            {
                Id = walkDomainModel.Id,
                Name = walkDomainModel.Name,
                Description = walkDomainModel.Description,
                LengthInKm = walkDomainModel.LengthInKm,
                WalkImageUrl = walkDomainModel.WalkImageUrl,
                DifficultyId = walkDomainModel.DifficultyId,
                RegionId = walkDomainModel.RegionId
            };

            return Ok(walkDto);
        }

        //-----------------------------------------------------------------------------------------------------
        // DELETE : DELETE Walk
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Find the walk by Id
            var walkDomainModel = await dbContext.Walks.FindAsync(id);

            // Check if the walk exists
            if (walkDomainModel == null)
            {
                return NotFound();
            }

            // Remove the walk
            dbContext.Walks.Remove(walkDomainModel);

            // Save changes
            await dbContext.SaveChangesAsync();

            // Map Domain Model to DTO
            var walkDto = new WalkDto
            {
                Id = walkDomainModel.Id,
                Name = walkDomainModel.Name,
                Description = walkDomainModel.Description,
                LengthInKm = walkDomainModel.LengthInKm,
                WalkImageUrl = walkDomainModel.WalkImageUrl,
                DifficultyId = walkDomainModel.DifficultyId,
                RegionId = walkDomainModel.RegionId
            };

            // Return the deleted walk
            return Ok(walkDto);
        }


        //--------------/--------------------------------------------------------------------------------------
    }
}


/*

Include()
Used to load related entities (navigation properties).
Returns the main entity (Walk) with its related objects populated.
var walks = await dbContext.Walks
    .Include(x => x.Region)
    .ToListAsync();

You can then do:

walk.Region.Name
Join()
Used when you want to manually combine data from two tables.
Returns whatever shape you specify (often an anonymous object or DTO).
var result = await dbContext.Walks
    .Join(dbContext.Regions,
          w => w.RegionId,
          r => r.Id,
          (w, r) => new
          {
              WalkName = w.Name,
              RegionName = r.Name
          })
    .ToListAsync();

This returns only the selected data, not full Walk objects with navigation properties.

Easy way to remember
Include() = "Load related objects."
Join() = "Combine tables into a custom result."










*/