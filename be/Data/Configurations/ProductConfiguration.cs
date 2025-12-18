using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using be.Models;

namespace be.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(r => r.id);
            builder.Property(r => r.name).IsRequired().HasMaxLength(20);
            builder.Property(r => r.id)
                   .HasDefaultValueSql("gen_random_uuid()");

            builder.HasData(
                new Product
                {
                    id = "9245fe4a-d402-451c-b9ed-3af38724d6e6",
                    name = "Sting",
                    price = 15000,
                    imageUrl = "https://th.bing.com/th/id/R.04aa037de876562b2b8208077a7f0e51?rik=j5vgIYzLLBM5rA&riu=http%3a%2f%2faefoodstore.com%2fcdn%2fshop%2ffiles%2fhaohotnew_4.png%3fv%3d1718745575&ehk=NfNEMkARCQ41vSr6hLbZ0JuZcmuTPAflQzkox72MP%2bc%3d&risl=&pid=ImgRaw&r=0"
                },
                new Product
                {
                    id = "b8fd818f-6d8e-4bc5-ac8b-3af38sssss6e6",
                    name = "Red Bull",
                    price = 20000,
                    imageUrl = "https://ceklist.id/wp-content/uploads/2022/12/2-Minuman-Berenergi-Kratingdaeng-Red-Bull.jpeg"
                },
                new Product
                {
                    id = Guid.NewGuid().ToString(),
                    name = "Coca Cola",
                    price = 12000,
                    imageUrl = "https://www.pngplay.com/wp-content/uploads/15/Regular-Coke-Can-Coca-Cola-Transparent-Background.png"
                },
                new Product
                {
                    id = Guid.NewGuid().ToString(),
                    name = "Pepsi",
                    price = 11000,
                    imageUrl = "https://www.pepsicopartners.com/medias/1200Wx1200H-1-HYK-24461.jpeg?context=bWFzdGVyfHJvb3R8NjI2OTB8aW1hZ2UvanBlZ3xhRFJtTDJoaU1DOHhNREE0TURNMU5qWTJNek15Tmk4eE1qQXdWM2d4TWpBd1NGOHhMVWhaU3kweU5EUTJNUzVxY0dWbnxlZmViMjA1ZDhmMTc4ODY4ODNjMDQ2YmM1ZjNjYTNlYmVkYTUwZmNjMmM2Mjc5MGQ5OWVjMjIxZDdmOTEyYjI3"
                },
                new Product
                {
                    id = Guid.NewGuid().ToString(),
                    name = "Mirinda",
                    price = 10000,
                    imageUrl = "https://th.bing.com/th/id/R.c9c3b8ec26d18604dff1828b8fc54cea?rik=m6L7Fsofb4LXmg&pid=ImgRaw&r=0"
                },
                new Product
                {
                    id = Guid.NewGuid().ToString(),
                    name = "Fanta",
                    price = 10000,
                    imageUrl = "https://www.coca-cola.com/content/dam/onexp/mv/home-images/fanta/Fanta-desktop.png"
                },
                new Product
                {
                    id = Guid.NewGuid().ToString(),
                    name = "Sprite",
                    price = 10000,
                    imageUrl = "https://pngimg.com/uploads/sprite/sprite_PNG98773.png"
                }
            );
        }
    }
}