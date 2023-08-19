using Microsoft.EntityFrameworkCore;

namespace FakeSurance.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Coverage> Coverages { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCoverage> ProductCoverages { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        


    }
}
