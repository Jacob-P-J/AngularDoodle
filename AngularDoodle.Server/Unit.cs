using System.ComponentModel.DataAnnotations;

namespace AngularDoodle.Server
{
    public class Unit
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string? name { get; set; }
        public string? casNumber { get; set; }
        [Required]
        public int amount { get; set; }
        [Required]
        public string? location { get; set; }
    }
}
