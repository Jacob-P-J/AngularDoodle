using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AngularDoodle.Server
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ChemicalUnit> ChemicalUnits { get; set; } 
    }

    public class ChemicalUnit
    {
        public Guid Id { get; set; }
        public string Name_DK { get; set; }
        public string CasNumber { get; set; }
        public decimal Amount { get; set; }
        public string Location { get; set; }
    }
     
    }