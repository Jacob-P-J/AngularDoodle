using System.ComponentModel.DataAnnotations;

namespace AngularDoodle.Server.Models
{
    // A model for a unit object
    public class Unit
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? CasNumber { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public string? Location { get; set; }
    }
}
