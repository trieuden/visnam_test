using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using be.Models;

namespace be.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.id);
            builder.Property(r => r.name).IsRequired().HasMaxLength(20);
            builder.Property(r => r.id)
                   .HasDefaultValueSql("gen_random_uuid()");

            builder.HasData(
                new Role
                {
                    id = "9245fe4a-d402-451c-b9ed-9c1a04247482",
                    name = "Admin"
                },
                new Role
                {
                    id = "b8fd818f-6d8e-4bc5-ac8b-3af38724d6e6",
                    name = "User"
                }
            );
        }
    }
}