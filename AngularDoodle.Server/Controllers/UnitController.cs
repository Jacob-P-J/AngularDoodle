using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AngularDoodle.Server;
using System.Reflection;
using System.Linq;

namespace AngularDoodle.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowAngularApp")]
    public class UnitController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UnitController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetUnits")]
        public async Task<IActionResult> GetUnits(
            [FromQuery] string sortColumn = "Id",
            [FromQuery] string sortDirection = "asc",
            [FromQuery] string searchName = "",
            [FromQuery] string searchCas = "",
            [FromQuery] decimal? searchAmount = null,
            [FromQuery] string searchLocation = "",
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 25)
        {
            try
            {
                var query = _context.ChemicalUnits.AsQueryable();

                // Filtrering
                if (!string.IsNullOrEmpty(searchName))
                    query = query.Where(u => u.Name_DK.Contains(searchName));

                if (!string.IsNullOrEmpty(searchCas))
                    query = query.Where(u => u.CasNumber.Contains(searchCas));

                if (searchAmount.HasValue)
                    query = query.Where(u => u.Amount <= searchAmount);

                if (!string.IsNullOrEmpty(searchLocation))
                    query = query.Where(u => u.Location.Contains(searchLocation));

                // Dynamisk sortering
                query = sortColumn.ToLower() switch
                {
                    "name" => sortDirection.ToLower() == "asc" ? query.OrderBy(u => u.Name_DK) : query.OrderByDescending( u =>  u.Name_DK),
                    "casnumber" => sortDirection.ToLower() == "asc" ? query.OrderBy(u => u.CasNumber) : query.OrderByDescending ( u => u.CasNumber),
                    "amount" => sortDirection.ToLower() == "asc" ? query.OrderBy(u => u.Amount) : query.OrderByDescending(u => u.Amount),
                    "location" => sortDirection.ToLower() == "asc" ? query?.OrderBy(u => u.Location) : query?.OrderByDescending(u => u.Location),
                    _ => query.OrderBy(u => u.Id),
                };

                // Paginering
                int totalUnits = await query.CountAsync();
                int totalPages = (int)Math.Ceiling(totalUnits / (double)pageSize);
                var pagedUnits = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

                return Ok(new { totalPages, pageNumber, units = pagedUnits });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}