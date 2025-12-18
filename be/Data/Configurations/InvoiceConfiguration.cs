using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using be.Models;

namespace be.Data.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(r => r.id);
            builder.Property(r => r.id)
                   .HasDefaultValueSql("gen_random_uuid()");

            builder.HasData(
                new Invoice
                {
                    id = "a1b2c3d4-e5f6-4789-abcd-ef0123456789",
                    userId = "5513783a-493e-436f-923f-42e783261685",
                    totalAmount = 30000,
                    dateCreated = DateTime.UtcNow
                }
            );
        }
    }
}