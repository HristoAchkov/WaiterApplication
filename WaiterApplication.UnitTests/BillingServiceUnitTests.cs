using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Core.Services;
using WaiterApplication.Infrastructure.Data.Common;
using WaiterApplication.Infrastructure.Data;
using WaiterApplication.Core.Models.ViewModels;
using WaiterApplication.Infrastructure.Data.Models;

namespace WaiterApplication.UnitTests
{
    [TestFixture]
    internal class BillingServiceUnitTests
    {
        private WaiterApplicationDbContext dbContext;
        private IRepository repository;
        private IBillingService billingService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<WaiterApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            dbContext = new WaiterApplicationDbContext(options);

            repository = new Repository(dbContext);

            billingService = new BillingService(repository);
        }
        [Test]
        public async Task CalculateBill_ShouldReturnCorrectTotalAmountAndSetTableStatusToFalse()
        {
            // Arrange
            var order = new Order { Id = 1, IsPaid = true, TableNumber = 1 };
            var table = new Table { Id = 1, TableName = "Table 1", Status = true };
            var dish1 = new Dish { Id = 1, Price = 10.00m };
            var dish2 = new Dish { Id = 2, Price = 15.00m };
            var orderDish1 = new OrderDish { OrderId = 1, DishId = 1, Quantity = 1 };
            var orderDish2 = new OrderDish { OrderId = 1, DishId = 2, Quantity = 1 };

            dbContext.AddRange(order, table, dish1, dish2, orderDish1, orderDish2);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await billingService.CalculateBill(1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(25.00m, result.TotalAmount); // 2 * 10.00 + 1 * 15.00 = 35.00

            var updatedTable = await repository.GetByIdAsync<Table>(1);
            Assert.IsFalse(updatedTable.Status);
        }

        [Test]
        public async Task CreateBill_ShouldCreateBillAndReturnIt()
        {
            // Arrange
            var model = new BillViewModel { OrderId = 1 };

            // Act
            var result = await billingService.CreateBill(model);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(1, result.OrderId);
            Assert.IsFalse(result.PaymentCompleted);
        }

        [Test]
        public async Task RemoveBill_ShouldRemoveBill()
        {
            // Arrange
            var bill = new Bill { Id = 4, OrderId = 4 };

            dbContext.Add(bill);
            await dbContext.SaveChangesAsync();

            // Act
            await billingService.RemoveBill(4);

            // Assert
            var result = await repository.GetByIdAsync<Bill>(4);
            Assert.Null(result);
        }

        [Test]
        public async Task BillDetailsByIdAsync_ShouldReturnBillDetails()
        {
            // Arrange
            var bill = new Bill { Id = 1, OrderId = 1 };

            dbContext.Add(bill);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await billingService.BillDetailsByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual(1, result.OrderId);
        }

        [Test]
        public async Task BillExistsAsync_ShouldReturnTrueIfBillExists()
        {
            // Arrange
            var bill = new Bill { Id = 2 };

            dbContext.Add(bill);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await billingService.BillExistsAsync("2");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task BillExistsAsync_ShouldReturnFalseIfBillDoesNotExist()
        {
            // Act
            var result = await billingService.BillExistsAsync("999");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task IsOrderPaid_ShouldMarkOrderAsPaid()
        {
            // Arrange
            var order = new Order { Id =8, IsPaid = false };

            dbContext.Add(order);
            await dbContext.SaveChangesAsync();

            // Act
            await billingService.IsOrderPaid(8);

            // Assert
            var updatedOrder = await repository.GetByIdAsync<Order>(8);
            Assert.IsTrue(updatedOrder.IsPaid);
        }
        [TearDown]
        public void TearDown()
        {
            this.dbContext.Database.EnsureDeleted();
            this.dbContext.DisposeAsync();
        }
    }
}
