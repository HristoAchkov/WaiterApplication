using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WaiterApplication.Infrastructure.Data.Models;

namespace WaiterApplication.Infrastructure.Data
{
    public class WaiterApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public WaiterApplicationDbContext(DbContextOptions<WaiterApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Dish> Dishes { get; set; } = null!;
        public DbSet<Table> Tables { get; set; } = null!;
        public DbSet<Bill> Bills { get; set; } = null!;
        public DbSet<InventoryItem> InventoryItems { get; set; } = null!;
        public DbSet<OrderDish> OrderDishes { get; set; } = null!;
    }
}