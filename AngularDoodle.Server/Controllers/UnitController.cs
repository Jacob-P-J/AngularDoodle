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
    // A controller that handles requests for units
    public class UnitController : ControllerBase
    {
        
        private readonly string _filePath = "Data/units.json";

        [HttpGet(Name = "GetUnits")]
        // GET request that returns a response object with a list of units and pagination information
        public async Task<IActionResult> GetUnits(
            [FromQuery] string sortColumn = "id",
            [FromQuery] string sortDirection = "asc",
            [FromQuery] string searchName = "",
            [FromQuery] string searchCas = "",
            [FromQuery] int? searchAmount = null,
            [FromQuery] string searchLocation = "",
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 25)
        {
            try
            {
                // Get the list of units from the JSON file
                var units = await GetUnitsFromJsonFile(searchName, searchCas, searchAmount, searchLocation);

                // Applies the sorting
                units = sortDirection == "asc"
                    ? units.OrderBy(unit => unit.GetType().GetProperty(sortColumn, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)?.GetValue(unit, null)).ToList()
                    : units.OrderByDescending(unit => unit.GetType().GetProperty(sortColumn, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)?.GetValue(unit, null)).ToList();


                // Applies the pagination
                int totalUnits = units.Count;
                int totalPages = (int)Math.Ceiling(totalUnits / (double)pageSize);
                var pagedUnits = units.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                // Create the response object
                var response = new
                {
                    totalPages,
                    pageNumber,
                    units = pagedUnits
                };

                return Ok(response);
            }

            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // Reads from a JSON file and returns a list of units based on search criteria
        private async Task<List<Unit>> GetUnitsFromJsonFile(string searchName, string searchCas, int? searchAmount, string searchLocation)
        {
            try
            {
                using (var reader = new StreamReader(_filePath))
                {
                    var jsonFile = await reader.ReadToEndAsync();
                    var units = JsonSerializer.Deserialize<List<Unit>>(jsonFile);

                    if (units == null)
                    {
                        return new List<Unit>();
                    }

                    if (!string.IsNullOrEmpty(searchName))
                    {
                        units = units.Where(u => u.Name?.Contains(searchName, StringComparison.OrdinalIgnoreCase) == true).ToList();
                    }

                    if (!string.IsNullOrEmpty(searchCas))
                    {
                        units = units.Where(u => u.CasNumber?.Contains(searchCas, StringComparison.OrdinalIgnoreCase) == true).ToList();
                    }

                    if (searchAmount.HasValue)
                    {
                        units = units.Where(u => u.Amount <= searchAmount).ToList();
                    }

                    if (!string.IsNullOrEmpty(searchLocation))
                    {
                        units = units.Where(u => u.Location?.Contains(searchLocation, StringComparison.OrdinalIgnoreCase) == true).ToList();
                    }

                    return units;
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
                throw;
            }
        }
    }
}