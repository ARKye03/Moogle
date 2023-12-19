using Microsoft.AspNetCore.Mvc;
using GraphEngine;
using MoogleEngine;

namespace YourNamespace.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class HelloController : ControllerBase
  {
    public SearchResult results = new();
    [HttpGet]
    public IActionResult Get([FromQuery] string expression)
    {
      results = Hello.Main(expression);
      return Ok(results);
    }
  }
}
