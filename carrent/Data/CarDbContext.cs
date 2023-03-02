using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using carrent.Data;

namespace carrent.Data
{
    public class CarDbContext : IdentityDbContext<Clieunt>
    {
        public CarDbContext(DbContextOptions<CarDbContext> options)
            : base(options)
        {
        }
        public DbSet<Car> Cars;
        public DbSet<Rezervation> Rezerations;
        public DbSet<BrandModel> Brands;
        public DbSet<carrent.Data.BrandModel> BrandModel { get; set; } = default!;
        public DbSet<carrent.Data.Car> Car { get; set; } = default!;
        public DbSet<carrent.Data.Rezervation> Rezervation { get; set; } = default!;
    }
}