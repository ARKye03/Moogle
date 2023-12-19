using Microsoft.AspNetCore.Mvc;
using MoogleEngine;

namespace Middle.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromQuery] string query)
        {
            // Call the Query function and return the result
            SearchResult result = Moogle.Query(query);
            return Ok(result);
        }
    }
}