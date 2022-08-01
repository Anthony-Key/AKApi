using AKAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AKAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<NationalPark> NationalParks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
