using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MobileMekaniko.Models;

namespace MobileMekaniko.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<CarMake> CarMakes { get; set; }    
        public DbSet<Model> Models { get; set; }
        public DbSet<CarModel> CarModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1-to-M Customer-Car
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Car)
                .WithOne(car => car.Customer)
                .HasForeignKey(car => car.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // M-to-M Car-Make
            modelBuilder.Entity<CarMake>()
                .HasKey(cmake => new { cmake.CarId, cmake.MakeId });

            modelBuilder.Entity<CarMake>()
                .HasOne(cmake => cmake.Car)
                .WithMany(car => car.CarMake)
                .HasForeignKey(cmake => cmake.CarId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CarMake>()
                .HasOne(cmake => cmake.Make)
                .WithMany(make => make.CarMake)
                .HasForeignKey(cmake => cmake.MakeId)
                .OnDelete(DeleteBehavior.Cascade);

            // M-to-M Car-Model
            modelBuilder.Entity<CarModel>()
                .HasKey(cmodel => new { cmodel.CarId, cmodel.ModelId });

            modelBuilder.Entity<CarModel>()
                .HasOne(cmodel => cmodel.Car)
                .WithMany(car => car.CarModel)
                .HasForeignKey(cmodel => cmodel.CarId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CarModel>()
                .HasOne(cmodel => cmodel.Model)
                .WithMany(model => model.CarModel)
                .HasForeignKey(cmodel => cmodel.ModelId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
