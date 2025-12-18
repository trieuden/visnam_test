using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using be.Models;

namespace be.Data.Configurations
{
    public class InvoiceDetailConfiguration : IEntityTypeConfiguration<InvoiceDetail>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
        {
            builder.HasKey(r => r.id);
            builder.Property(r => r.id)
                   .HasDefaultValueSql("gen_random_uuid()");

            builder.HasData(
                new InvoiceDetail
                {
                    id = "a1b2c3d4-e5f6-4789-abcd-ef0123456790",
                    invoiceId = "a1b2c3d4-e5f6-4789-abcd-ef0123456789",
                    productId = "9245fe4a-d402-451c-b9ed-3af38724d6e6",
                    quantity = 2,
                }
            );
        }
    }
}