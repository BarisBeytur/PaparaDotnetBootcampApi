using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaparaDotnetBootcampApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaDotnetBootcampApi.DataAccess.Configuration
{
    /// <summary>
    /// Müşteri tablosu için konfigürasyon ayarları
    /// </summary>
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder
                .HasMany(x => x.Cards)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Property(x => x.Name)
                .IsRequired(true)
                .HasMaxLength(100);

            builder.Property(x => x.Surname)
                .IsRequired(true)
                .HasMaxLength(100);

            builder.Property(x => x.TCKN)
                .IsRequired(true)
                .HasMaxLength(11);

        }
    }
}
