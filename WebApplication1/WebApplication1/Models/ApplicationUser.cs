using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models
{
    public class ApplicationUser : IdentityUser
    {
        public String FirstName { get; set; }

        public String LastName { get; set; }

    }
}
