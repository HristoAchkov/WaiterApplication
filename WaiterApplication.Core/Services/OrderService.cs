using Microsoft.EntityFrameworkCore;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Infrastructure.Data.Common;
using WaiterApplication.Infrastructure.Data.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using WaiterApplication.Core.Models.ViewModels;
using Microsoft.EntityFrameworkCore.Query;

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

        public async Task<int> CreateOrderAsync(int tableId, OrderDish dish)
        {
            // Create a new order instance
            Order order = new Order
            {
                TableNumber = tableId,
            };

            // Perform any additional operations (e.g., calculate total amount) if necessary

            // Add the order to the database
            repository.AddAsync<Order>(order);
            await repository.SaveChangesAsync();

            // Return the ID of the newly created order
            return order.Id;
        }
        public async Task<bool> AddDishToOrderAsync(int orderId, int dishId)
        {
            // Retrieve the order from the database
            var order = await repository.GetByIdAsync<Order>(orderId);
            if (order == null)
            {
                // Order not found
                return false;
            }

            // Retrieve the dish from the database
            var dish = await repository.GetByIdAsync<Dish>(dishId);
            if (dish == null)
            {
                // Dish not found
                return false;
            }
            OrderDish orderDish = new OrderDish()
            {
                Id = dish.Id,
                OrderId = orderId,
            };
            // Add the dish to the order's list of ordered dishes
            order.OrderedDishes.Add(orderDish);

            // Save changes to the database
            await repository.SaveChangesAsync();

            return true;
        }
        public async Task<Order> GetOrderDetailsAsync(int orderId)
        {
            // Retrieve the order from the database
            var order = await repository
                .GetByIdAsync<Order>(orderId);

            return order;
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

        public async Task<Dish> CreateAsync(string name, string image, decimal price)
        {
            Dish dish = new Dish()
            {
                Name = name,
                Image = image,
                Price = price
            };
            await repository.AddAsync<Dish>(dish);
            await repository.SaveChangesAsync();
            return dish;
        }

        public async Task AddDishAsync(Dish dish, int tableId)
        {
            var order = new Order();
            //order.OrderedDishes.Add(dish);
            order.TableNumber = tableId;
        }
    }
}
