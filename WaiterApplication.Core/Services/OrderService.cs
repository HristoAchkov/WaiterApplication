using Microsoft.EntityFrameworkCore;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Infrastructure.Data.Common;
using WaiterApplication.Infrastructure.Data.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using WaiterApplication.Core.Models.ViewModels;

namespace WaiterApplication.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository repository;

        public OrderService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<List<AllOrdersViewModel>> AllOrdersAsync()
        {
            var allOrders = repository.AllAsNoTracking<Order>()
                .Select(o => new AllOrdersViewModel()
                {
                    TableNumber = o.Table.TableName,
                    TotalAmount = o.TotalAmount
                })
                .ToListAsync();

            return await allOrders;
        }

        public async Task CreateAsync(ICollection<Dish> ordered)
        {
            await repository.AddAsync(new Order()
            {
                OrderedDishes = ordered
            });

            await repository.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(string id)
        {
            return await repository.AllAsNoTracking<Order>()
                .AnyAsync(o => o.Id.ToString() == id);
        }

        public async Task<bool> ItemExistsByIdAsync()
        {
            return true;
        }
    }
}
