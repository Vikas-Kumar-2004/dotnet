using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks_ASP.NET_Core.Data;
using NZWalks_ASP.NET_Core.Models.Domain;
using NZWalks_ASP.NET_Core.Models.DTO;
using Microsoft.EntityFrameworkCore;
using NZWalks_ASP.NET_Core.Repositories;

namespace NZWalks_ASP.NET_Core.Controllers
{
    // https:localhost:1234/api/
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }

        // GET ALL  REGION (Get All Region )
        [HttpGet]


        public async Task<IActionResult> GetAll()
        {
            // Get data from the database (Domain Models)
            var regionsDomain = await dbContext.Regions.ToListAsync();

            // Map Domain Models to DTOs
            // Never return Domain Models directly to the client.
            var regionsDto = new List<RegionDto>();

            foreach (var region in regionsDomain)
            {
                regionsDto.Add(new RegionDto
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });
            }

            return Ok(regionsDto);
        }


        // GET SINGLE REGION (Get Region By ID)

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Get the region from the database
            var regionDomain = await dbContext.Regions
                .FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return Ok(regionDto);
        }

        // POST: Create a New Region

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            // Add the entity to the database
            await dbContext.Regions.AddAsync(regionDomainModel);

            // Save changes
            await dbContext.SaveChangesAsync();

            // Map Domain Model back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            // Return HTTP 201 Created
            return CreatedAtAction(
                nameof(GetById),
                new { id = regionDto.Id },
                regionDto);
        }

        // PUT: Update a Region

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            // Check if the region exists
            var regionDomainModel = await dbContext.Regions
                .FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Map DTO to Domain Model
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            // Save changes
            await dbContext.SaveChangesAsync();

            // Map Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }


        // DELETE: Delete a Region

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Find the region by Id
            var regionDomainModel = await dbContext.Regions.FindAsync(id);

            // Check if the region exists
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Remove the region
            dbContext.Regions.Remove(regionDomainModel);

            // Save changes
            await dbContext.SaveChangesAsync();

            // Map Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            // Return the deleted region
            return Ok(regionDto);
        }

    }
}

/*
 
 
 
 
 | `Find(id)`                                                                                                               | `FirstOrDefault(x => x.Id == id)`                                                                  |
| ------------------------------------------------------------------------------------------------------------------------ | -------------------------------------------------------------------------------------------------- |
| Searches by **primary key only**.                                                                                        | Searches using **any condition**.                                                                  |
| Very fast for primary key lookups.                                                                                       | Executes the LINQ query you provide.                                                               |
| Checks EF Core's **change tracker** first. If the entity is already loaded, it returns it without querying the database. | Always translates the LINQ query into SQL (unless the query is already being evaluated in memory). |
| Can only search by the entity's primary key.                                                                             | Can search by any property or combination of properties.                                           |
| Simpler syntax.                                                                                                          | More flexible syntax.                                                                              |

 
 
 -----------------------------------------------------------------------------

What is Task?
Yes. Task is the return type of an asynchronous method. 
A Task represents work that is currently running or will finish in the future.

Before async---------------

A normal method executes completely and returns the final result.Here, the return type is: IActionResult

It means:"This method will return an IActionResult immediately."

After async--------------

Now the return type is: Task<IActionResult>

This means: "This method will eventually return an IActionResult, but it may need to wait for an asynchronous operation to complete first."
 
 
 
 
 */