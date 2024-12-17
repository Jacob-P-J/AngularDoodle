using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using System.Reflection;
using System.Text.Json;

using AngularDoodle.Server.Models;

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
        public IActionResult Get([FromQuery] string sortColumn = "id", [FromQuery] string sortDirection = "asc")
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

            PropertyInfo? property = typeof(Unit).GetProperty(sortColumn, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
            {
                return BadRequest($"Invalid sort column: {sortColumn}");
            }

            units = sortDirection.ToLower() == "asc"
                ? units.OrderBy(u => property.GetValue(u, null)).ToList()
                : units.OrderByDescending(u => property.GetValue(u, null)).ToList();

            return Ok(units);
        }
    }
}
