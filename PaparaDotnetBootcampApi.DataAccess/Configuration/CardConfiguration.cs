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
    /// Kart entity'sinin veritabanı tablosu üzerindeki konfigürasyonları
    /// </summary>
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Cards)
                .HasForeignKey(x => x.CustomerId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.CardNumber)
                .IsRequired(true)
                .HasMaxLength(16);

            builder.Property(x => x.Cvv)
                .IsRequired(true)
                .HasMaxLength(3);

            builder.Property(x => x.ExpiryDate)
                .IsRequired(true);

            builder.Property(x => x.NameSurname)
                .HasMaxLength(100)
                .IsRequired(true);

            builder.Property(x => x.ExpiryDate)
                .HasMaxLength(5) // 01/24 vb.
                .IsRequired(true);



        }
    }
}
