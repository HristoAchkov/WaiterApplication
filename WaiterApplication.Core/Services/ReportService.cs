using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Core.Models.ViewModels;
using WaiterApplication.Infrastructure.Data.Common;
using WaiterApplication.Infrastructure.Data.Models;

namespace WaiterApplication.Core.Services
{
    public class ReportService : IReportService
    {
        private readonly IRepository repository;

        public ReportService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<TopDishesModel> GetTopDishes()
        {
            var orders = repository.AllAsNoTracking<Order>()
       .Where(o => o.IsPaid == true)
       .Include(o => o.OrderedDishes)
           .ThenInclude(od => od.Dish);

            var model = new TopDishesModel();
            var dishOrders = new Dictionary<string, int>();

            foreach (var order in orders)
            {
                foreach (var orderDish in order.OrderedDishes)
                {
                    if (dishOrders.ContainsKey(orderDish.Dish.Name))
                    {
                        dishOrders[orderDish.Dish.Name] += orderDish.Quantity;
                    }
                    else
                    {
                        dishOrders[orderDish.Dish.Name] = orderDish.Quantity;
                    }
                }
            }

            var orderedDishOrders = dishOrders.OrderByDescending(kv => kv.Value);

            model.DishNames = orderedDishOrders.Select(kv => kv.Key).ToList();
            model.TimesOrdered = orderedDishOrders.Select(kv => kv.Value).ToList();

            return model;
        }

        public async Task<decimal> TotalAmountEarned()
        {
            var orders = repository.AllAsNoTracking<Order>()
        .Where(o => o.IsPaid == true)
        .Include(o => o.OrderedDishes)
            .ThenInclude(od => od.Dish);

            decimal totalAmountEarned = 0;

            foreach (var order in orders)
            {
                var orderPrice = order.OrderedDishes.Sum(x => x.Dish.Price);
                totalAmountEarned += orderPrice;
            }

            return totalAmountEarned;
        }
        public async Task<TopTablesModel> GetTopTables()
        {
            var orders = await repository.AllAsNoTracking<Order>()
                .Where(o => o.IsPaid)
                .Include(o => o.Table)
                .ToListAsync();

            var groupedOrders = orders
                .GroupBy(o => o.Table.TableName)
                .Select(g => new { TableNumber = g.Key, OrderCount = g.Count() })
                .OrderByDescending(x => x.OrderCount)
                .Take(3)
                .ToList();

            var model = new TopTablesModel
            {
                TableNumber = groupedOrders.Select(x => x.TableNumber).ToList(),
                OrderCount = groupedOrders.Select(x => x.OrderCount).ToList()
            };

            return model;
        }
    }
}
