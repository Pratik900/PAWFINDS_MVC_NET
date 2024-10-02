using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using WebApplication1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Razorpaycore8.Models;

namespace WebApplication1.Data
{
    public class AppDbContext: IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<PetDetail> PetDetails { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<VeterinaryDr> VeterinaryDrs { get; set; }

        public DbSet<PetFood> PetFoods { get; set;}
        public DbSet<MerchantOrder> MerchantOrders { get; set; }
    }
}
