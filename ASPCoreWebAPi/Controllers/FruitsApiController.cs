using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPCoreWebAPi.Controllers
{
    [Route("api/[controller]")] // Base URL: https://localhost:7158/api/FruitsApi
    [ApiController]
    public class FruitsApiController : ControllerBase
    {
        public List<string> fruits = new List<string>()
        {
            "Apple",
            "Banana",
            "Cherry",
            "Grapes"
        };

        // GET: https://localhost:7158/api/FruitsApi
        [HttpGet]
        public List<string> GetFruits()
        {
            return fruits;
        }

        // GET: https://localhost:7158/api/FruitsApi/1
    
        [HttpGet("{id}")]
        public string GetFruitsByIndex(int id)
        {
            return fruits.ElementAt(id);
        }

        // POST: https://localhost:7158/api/FruitsApi
        // Request Body:
        // "Mango"
        [HttpPost]
        public List<string> AddFruit([FromBody] string fruit)
        {
            fruits.Add(fruit);
            return fruits;
        }
    }
}