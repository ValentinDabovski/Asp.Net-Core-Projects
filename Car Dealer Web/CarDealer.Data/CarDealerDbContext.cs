namespace CarDealer.Data
{
    using Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class CarDealerDbContext : IdentityDbContext<User>
    {
        public CarDealerDbContext(DbContextOptions<CarDealerDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Car> Cars { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Part> Parts { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<PartCar> PartCars { get; set; }    
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PartCar>()
                .HasOne(pc => pc.Car)
                .WithMany(c => c.PartCars)
                .HasForeignKey(pc => pc.CarId);


            builder.Entity<PartCar>()
                .HasOne(pc => pc.Part)
                .WithMany(part => part.PartCars)
                .HasForeignKey(pc => pc.PartId);



            builder.Entity<PartCar>()
                .HasKey(pc => new {pc.CarId, pc.PartId});

            builder.Entity<Sale>()
                .HasOne(sale => sale.Car)
                .WithMany(car => car.Sales)
                .HasForeignKey(sale => sale.CarId);


            builder.Entity<Sale>()
                .HasOne(sale => sale.Customer)
                .WithMany(customer => customer.Sales)
                .HasForeignKey(sale => sale.CustomerId);

            builder.Entity<Supplier>()
                .HasMany(sale => sale.Parts)
                .WithOne(part => part.Supplier)
                .HasForeignKey(sale => sale.SupplierId);


            base.OnModelCreating(builder);
        }
    }
}
