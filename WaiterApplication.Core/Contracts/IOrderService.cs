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
        Task<Table> GetTableAsync(int tableId);
        Task CreateOrderDish(int orderId, int dishId,string? comment);
        Task CreateOrderAsync(Order order);
        Task AddOrderDishToOrder(OrderDish orderedDish, int orderId);
        Task<Dish> GetDishDetailsByIdAsync(int id);
        Task<OrderViewModel> GetOrderDetailsByIdAsync(int id);
        Task<List<AllDishesOrder>> AllDishes(int orderId);
        Task RemoveOrderDishAndOrder(int tableId,int orderId, List<int> dishIds);
        Task RemoveOrderDishFromOrder(int dishId);
        Task<Order> GetOrder(int orderId);
    }
}
