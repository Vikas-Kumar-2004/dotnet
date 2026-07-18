using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        // POST: Create Walk
        [HttpPost]
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

    }
}
