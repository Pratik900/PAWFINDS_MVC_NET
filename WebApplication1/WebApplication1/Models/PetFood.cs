using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class PetFood
    {
        [Key]
        public Guid Id { get; set; }
        public string? ProductName { get; set; }
        public string? Brand { get; set; }

        public string? Species { get; set; }

        public long? Calories { get; set; }

        public string? PackageSize { get; set; }

        public long? Prize { get; set; }

        public DateOnly ExpDate { get; set; }

        public string? Certifications { get; set;}

    }
}
