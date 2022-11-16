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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Property>().HasData(
                new Property
                {
                    Id = 1,
                    Name = "11 James St",
                    Suburb = "Heidelberg",
                    PostCode = "3084",
                    State = "VIC",
                    ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa3.jpg",
                    AskingPrice = 980000,
                    Area = 591,
                    NoBedroom = 3,
                    NoToilet = 4,
                    CreatedDate = DateTime.Now
                },
                new Property
                {
                    Id = 2,
                    Name = "16 Lily Crt",
                    Suburb = "Frankston",
                    PostCode = "2011",
                    State = "NSW",
                    ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa3.jpg",
                    AskingPrice = 1080000,
                    Area = 750,
                    NoBedroom = 4,
                    NoToilet = 2,
                    CreatedDate = DateTime.Now
                },
                new Property
                {
                    Id = 3,
                    Name = "177 Wonderwomen Prd",
                    Suburb = "Hans",
                    PostCode = "4011",
                    State = "QLD",
                    ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa3.jpg",
                    AskingPrice = 688000,
                    Area = 720,
                    NoBedroom = 4,
                    NoToilet = 2,
                    CreatedDate = DateTime.Now
                }
                );
        }
    }
}
