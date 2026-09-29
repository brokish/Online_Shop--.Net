using Microsoft.EntityFrameworkCore;
using Online_Shop.DbConnection;
using Online_Shop.services; // 1. დაამატე ეს

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// 2. დაამატე ეს ხაზი, რომ DI-მ იცოდეს სერვისის შექმნა
builder.Services.AddScoped<OnlineShopServices>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();