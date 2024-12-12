using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AngularDoodle.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowAngularApp")]
    public class UnitController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public UnitController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet(Name = "GetUnits")]
        public IActionResult Get([FromQuery] int page = 1, [FromQuery] int pageSize = 25)
        {
            var filePath = Path.Combine(_env.ContentRootPath, "Data", "units.json");
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("The units.json file was not found.");
            }
            var jsonData = System.IO.File.ReadAllText(filePath);
            List<Unit>? units;
            try 
            {
                units = JsonSerializer.Deserialize<List<Unit>>(jsonData);
            }
            catch (JsonException ex)
            {
                return BadRequest($"Error deserializing JSON data: {ex.Message}");
            }


            if (units == null)
            {
                return NotFound();
            }

            var paginatedUnits = units.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return Ok(paginatedUnits);
        }
    }
}
