using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using be.Models;
using BCrypt.Net;

namespace be.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.id);

            builder.Property(u => u.id)
                   .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(u => u.username).IsRequired().HasMaxLength(50);
            builder.Property(u => u.email).IsRequired().HasMaxLength(100);

            builder.HasOne(u => u.role)
                   .WithMany(r => r.users)
                   .HasForeignKey(u => u.roleId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new User
                {
                    id = "5513783a-493e-436f-923f-42e783261685",
                    username = "admin",
                    email = "admin@gmail.com",
                    passwordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    roleId = "9245fe4a-d402-451c-b9ed-9c1a04247482"
                },
                new User
                {
                    id = "3a3390c5-e51f-495c-939e-4c7401d89b14",
                    username = "user",
                    email = "user@gmail.com",
                    passwordHash = BCrypt.Net.BCrypt.HashPassword("user123"),
                    roleId = "b8fd818f-6d8e-4bc5-ac8b-3af38724d6e6"
                }
            );
        }
    }
}