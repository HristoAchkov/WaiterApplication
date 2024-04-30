using Microsoft.EntityFrameworkCore;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Infrastructure.Data.Common;
using WaiterApplication.Infrastructure.Data.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using WaiterApplication.Core.Models.ViewModels;
using Microsoft.EntityFrameworkCore.Query;
using WaiterApplication.Core.Models.QueryModels;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace WaiterApplication.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository repository;

        public OrderService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task AddOrderDishToOrder(OrderDish orderedDish, int orderId)
        {
            Order order = await repository.GetByIdAsync<Order>(orderId);

            order.OrderedDishes.Add(orderedDish);

            await repository.SaveChangesAsync();
        }

        public async Task<List<AllDishesOrder>> AllDishes(int orderId)
        {
            var dishesToShow = await repository.AllAsNoTracking<Dish>()
                .Select(x => new AllDishesOrder
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Image = x.Image,
                    OrderId = orderId
                }).ToListAsync();

            return dishesToShow;
        }

        public async Task<List<AllOrdersViewModel>> AllOrdersAsync()
        {
            var allOrders = repository.AllAsNoTracking<Order>()
                .Where(x => x.IsPaid == false)
                .Select(o => new AllOrdersViewModel()
                {
                    OrderId = o.Id,
                    TableNumber = o.Table.TableName,
                    TotalAmount = o.TotalAmount,
                    TableId = o.Table.Id,
                    CreatedOn = o.CreatedOn
                })
                .ToListAsync();

            return await allOrders;
        }

        public async Task CreateOrderAsync(Order order)
        {
            await repository.AddAsync<Order>(order);
            await repository.SaveChangesAsync();
        }

        public async Task CreateOrderDish(int orderId, int dishId, string? comment)
        {
            var orderDish = new OrderDish()
            {
                DishId = dishId,
                OrderId = orderId,
                Comment = comment,
            };

            await repository.AddAsync<OrderDish>(orderDish);
            await repository.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(string id)
        {
            return await repository.AllAsNoTracking<Order>()
                .AnyAsync(o => o.Id.ToString() == id);
        }

        public async Task<OrderViewModel> GetOrderDetailsByIdAsync(int id)
        {
            var order = await repository.GetByIdAsync<Order>(id);

            var orderDish = await repository.AllAsNoTracking<OrderDish>()
                .Where((od => od.OrderId == id)).ToListAsync();

            var orderViewModel = new OrderViewModel()
            {
                OrderId = order.Id,
                TableId = order.TableNumber,
                OrderDishes = orderDish,
                CreatedOn = order.CreatedOn
            };

            return orderViewModel;
        
        }
        public async Task<Dish> GetDishDetailsByIdAsync(int id)
        {
            var dish = await repository.GetByIdAsync<Dish>(id);

            return dish;
        }

        public async Task<Table> GetTableAsync(int tableId)
        {
            var table = await repository.GetByIdAsync<Table>(tableId);

            return table;
        }

        public async Task RemoveOrderDishAndOrder(int tableid,int orderId, List<int> dishIds)
        {
            var table = await repository.GetByIdAsync<Table>(tableid);
            table.Status = false;

            foreach (var id in dishIds)
            {
                await repository.RemoveAsync<OrderDish>(id);
            }
            await repository.RemoveAsync<Order>(orderId);
            await repository.SaveChangesAsync();
        }

        public async Task RemoveOrderDishFromOrder(int dishId)
        {
            await repository.RemoveAsync<OrderDish>(dishId);
            await repository.SaveChangesAsync();
        }
        public async Task<Order> GetOrder(int orderId)
        {
            var order = await repository.GetByIdAsync<Order>(orderId);

            return order;
        }
    }
}
