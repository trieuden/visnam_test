using be.Data;
using be.Services.Implements;
using be.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using be.Hubs;
using be.Configurations;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

// Cấu hình kết nối DB PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IInvoiceDetailService, InvoiceDetailService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        {
            var feUrl = builder.Configuration["FE_URL"];
            var origins = feUrl?.Split(',') ?? new string[] { "http://localhost:5173" };

            policy.WithOrigins(origins)
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();

        });
});

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddCustomSwagger();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseCors("AllowFrontend");


// MIGRATION --
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Lỗi kết nối DB: {ex.Message}");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<OrderHub>("/hub/orders");

app.Run();