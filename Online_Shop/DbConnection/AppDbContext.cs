using Microsoft.EntityFrameworkCore;
using Online_Shop.DbModels;

namespace Online_Shop.DbConnection;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Products> Products { get; set; }
    public DbSet<User> Users { get; set; }
}