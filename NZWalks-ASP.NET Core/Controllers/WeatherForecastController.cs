/*
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks_ASP.NET_Core.Models.Domain;

namespace NZWalks_ASP.NET_Core.Controllers
{
    // https:localhost:1234/api/
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        // now we create action method 
        //[HttpGet] // GET: https:localhost:1234/api/regions
        public IActionResult GetAll()
        {
            var regions = new List<Region>
            {


            new Region
            {
                Id = Guid.NewGuid(),
                Name = "vikas kumar",
                Code = "VKL",
                RegionImageUrl = "https://www.google.com/imgres?q=pinterest%20images&imgurl=https%3A%2F%2Fi.pinimg.com%2F736x%2Ff1%2Fc2%2Fa9%2Ff1c2a9dd2b6ec0270f628a1979e8a180.jpg&imgrefurl=https%3A%2F%2Fwww.pinterest.com%2Fpin%2Faesthetic-wallpaper-pinterest--744008800962098502%2F&docid=JT3zdhzs_5E0NM&tbnid=2BCfrwXrZHonLM&vet=12ahUKEwiBqaul69OVAxVjWXADHaoCCLEQnPAOegQIOhAA..i&w=675&h=1200&hcb=2&ved=2ahUKEwiBqaul69OVAxVjWXADHaoCCLEQnPAOegQIOhAA"

            },

            new Region
            {
                Id = Guid.NewGuid(),
                Name = "Sachin kumar",
                Code = "SKl",
                RegionImageUrl = "https://www.google.com/imgres?q=pinterest%20images&imgurl=https%3A%2F%2Fi.pinimg.com%2F736x%2Ff1%2Fc2%2Fa9%2Ff1c2a9dd2b6ec0270f628a1979e8a180.jpg&imgrefurl=https%3A%2F%2Fwww.pinterest.com%2Fpin%2Faesthetic-wallpaper-pinterest--744008800962098502%2F&docid=JT3zdhzs_5E0NM&tbnid=2BCfrwXrZHonLM&vet=12ahUKEwiBqaul69OVAxVjWXADHaoCCLEQnPAOegQIOhAA..i&w=675&h=1200&hcb=2&ved=2ahUKEwiBqaul69OVAxVjWXADHaoCCLEQnPAOegQIOhAA"

            },
            new Region
            {
                Id = Guid.NewGuid(),
                Name = "Pranshu Pandey",
                Code = "PKl",
                RegionImageUrl = "https://www.google.com/imgres?q=pinterest%20images&imgurl=https%3A%2F%2Fi.pinimg.com%2F736x%2Ff1%2Fc2%2Fa9%2Ff1c2a9dd2b6ec0270f628a1979e8a180.jpg&imgrefurl=https%3A%2F%2Fwww.pinterest.com%2Fpin%2Faesthetic-wallpaper-pinterest--744008800962098502%2F&docid=JT3zdhzs_5E0NM&tbnid=2BCfrwXrZHonLM&vet=12ahUKEwiBqaul69OVAxVjWXADHaoCCLEQnPAOegQIOhAA..i&w=675&h=1200&hcb=2&ved=2ahUKEwiBqaul69OVAxVjWXADHaoCCLEQnPAOegQIOhAA"

            }
            };
            return Ok(regions);

        }

    }
}

*/