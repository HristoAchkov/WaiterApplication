using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Core.Models.ViewModels;
using WaiterApplication.Infrastructure.Data.Common;
using WaiterApplication.Infrastructure.Data.Models;

namespace WaiterApplication.Core.Contracts
{
    public interface IOrderService
    {
        Task<bool> ExistsByIdAsync(string id);
        Task<List<AllOrdersViewModel>> AllOrdersAsync();
        Task<int> CreateOrderAsync(int tableId, OrderDish dish);
        Task<Dish> CreateAsync(string name, string image, decimal price);
        Task<bool> AddDishToOrderAsync(int orderId, int dishId);
        Task<Order> GetOrderDetailsAsync(int orderId);
        Task AddDishAsync(Dish dish, int tableId);
    }
}
