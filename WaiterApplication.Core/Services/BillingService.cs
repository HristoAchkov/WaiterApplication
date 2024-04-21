using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Core.Models.ViewModels;
using WaiterApplication.Infrastructure.Data.Common;
using WaiterApplication.Infrastructure.Data.Models;
using Table = WaiterApplication.Infrastructure.Data.Models.Table;

namespace WaiterApplication.Core.Services
{
    public class BillingService : IBillingService
    {
        private readonly IRepository repository;

        public BillingService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<BillViewModel> CalculateBill(int orderId)
        {
            var order = await repository.GetByIdAsync<Order>(orderId);

            var table = await repository.GetByIdAsync<Table>(order.TableNumber);

            var orderDish = await repository.AllAsNoTracking<OrderDish>()
                .Where((od => od.OrderId == orderId)).ToListAsync();

            List<Dish> dishes = new List<Dish>();

            foreach (var item in orderDish)
            {
               var dish = await repository.GetByIdAsync<Dish>(item.DishId);
                dishes.Add(dish);
            }

            BillViewModel model = new BillViewModel()
            {
                OrderId = order.Id,
                TotalAmount = dishes.Sum(d => d.Price)
            };

            table.Status = false;

            return model;
        }

        public async Task<Bill> CreateBill(BillViewModel model)
        {
            var bill = new Bill()
            {
                OrderId = model.OrderId,
                PaymentCompleted = false
            };

            await repository.AddAsync<Bill>(bill);
            await repository.SaveChangesAsync();

            return bill;
        }
        public async Task RemoveBill(int billId)
        {
            await repository.RemoveAsync<Bill>(billId);
            await repository.SaveChangesAsync();
        }
        public async Task<BillViewModel> BillDetailsByIdAsync(int billId)
        {
            var bill = await repository.GetByIdAsync<Bill>(billId);

            BillViewModel billDetails = new BillViewModel()
            {
                Id = bill.Id,
                OrderId = bill.OrderId
            };

            return billDetails;
        }

        public async Task<bool> BillExistsAsync(string billId)
        {
            return await repository.AllAsNoTracking<Bill>()
                .AnyAsync(d => d.Id.ToString() == billId);
        }

        public async Task IsOrderPaid(int orderId)
        {
            var order = await repository.GetByIdAsync<Order>(orderId);

            if (order.IsPaid == false)
            {
                order.IsPaid = true;
                await repository.SaveChangesAsync();
            }
        }
    }
}
