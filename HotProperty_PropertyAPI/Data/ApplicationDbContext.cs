using HotProperty_PropertyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotProperty_PropertyAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {

        }
        public DbSet<Property> Properties { get; set; }
    }
}
