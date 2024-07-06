using Microsoft.EntityFrameworkCore;
using PaparaDotnetBootcampApi.DataAccess.Configuration;
using PaparaDotnetBootcampApi.Entities;

namespace PaparaDotnetBootcampApi.DataAccess.Context
{
    /// <summary>
    /// Bu sınıf veritabanı işlemlerini yapmak için kullanılır.
    /// </summary>
    public class PatikaCohortContext : DbContext
    {

        public PatikaCohortContext(DbContextOptions<PatikaCohortContext> dbContextOptions) : base(dbContextOptions)
        {
      
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Card> Cards { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new CardConfiguration());
        }
    }
}
