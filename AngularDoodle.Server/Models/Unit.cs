using System.ComponentModel.DataAnnotations;



namespace AngularDoodle.Server.Models
{
    // A model for a unit object
    public class Unit
    {
        [Required]
        public required int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        public string? CasNumber { get; set; }
        [Required]
        public required int Amount { get; set; }
        [Required]
        public required string AmountUnit { get; set; }
        [Required]
        public required string Location { get; set; }
    }
}
