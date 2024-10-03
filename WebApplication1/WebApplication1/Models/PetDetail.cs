using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class PetDetail
    {
        [Key]
        public Guid Id { get; set;}
        public string? PetName { get; set;}
        public string? Species { get; set;}
        public string? Breed { get; set;}
        public int Age { get; set;}
        public string? Gender { get; set;}
        public string? Colour { get; set;}
        public string? OwnerName { get; set;}
        public long? Ownercontact { get; set;}
        public long? price { get; set;}

        public bool? IsAdopted { get;set;}=false;
    }
}
