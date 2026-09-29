using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Online_Shop.DbConnection;
using Online_Shop.DbModels;

namespace Online_Shop.services;

public class OnlineShopServices(IServiceScopeFactory scopeFactory)
{
    public async Task<User?> GetUserAsync()
    {
        using (var scope = scopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            return await context.Users.FirstOrDefaultAsync();
        }
    }

    public async Task<decimal> GetBalanceAsync()
    {
        await Task.Delay(1000);
        using (var scope = scopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            return await context.Users.Select(u => u.Balance).FirstOrDefaultAsync();
        }
    }

    public async Task<List<string>> GetProductsAsync()
    {
        await Task.Delay(3000);

        using (var scope = scopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            return await context.Products.Select(products => products.Name).ToListAsync();
        } 
    }
    
    public async Task<object> GetSequentialDashboardAsync()
    {
        var stopwatch = Stopwatch.StartNew();

        var user = await GetUserAsync();
        var balance = await GetBalanceAsync();
        var products = await GetProductsAsync();

        stopwatch.Stop();

        return new
        {
            UserName = user?.Name ?? "Unknown",
            Balance = balance,
            Products = products,
            ExecutionTimeInSeconds = stopwatch.Elapsed.TotalSeconds
        };
    }

    public async Task<object> GetParallelDashboardAsync()
    {
        var stopwatch = Stopwatch.StartNew();

        var userTask = GetUserAsync();
        var balanceTask = GetBalanceAsync();
        var productsTask = GetProductsAsync();

        await Task.WhenAll(userTask, balanceTask, productsTask);

        stopwatch.Stop();

        return new
        {
            UserName = (await userTask)?.Name ?? "Unknown",
            Balance = await balanceTask,
            Products = await productsTask,
            ExecutionTimeInSeconds = stopwatch.Elapsed.TotalSeconds
        };
    }
}