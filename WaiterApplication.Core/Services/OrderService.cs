using Microsoft.EntityFrameworkCore;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Infrastructure.Data.Common;
using WaiterApplication.Infrastructure.Data.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace WaiterApplication.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository repository;

        public OrderService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task CreateAsync(ICollection<OrderDish> ordered)
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
