using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Infrastructure.Data.Common;
using WaiterApplication.Infrastructure.Data;
using WaiterApplication.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using WaiterApplication.Core.Services;

namespace WaiterApplication.UnitTests
{
    [TestFixture]
    internal class ReportServiceUnitTests
    {
        private WaiterApplicationDbContext dbContext;
        private IRepository repository;
        private IReportService reportService;


        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<WaiterApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            dbContext = new WaiterApplicationDbContext(options);

            repository = new Repository(dbContext);
            reportService = new ReportService(repository);
        }
        [Test]
        public async Task GetTopDishes_ShouldReturnTopDishesOrderedByFrequency()
        {
            // Arrange
            var order1 = new Order { Id = 1, IsPaid = true };
            var order2 = new Order { Id = 2, IsPaid = true };
            var dish1 = new Dish { Id = 1, Name = "Dish 1" };
            var dish2 = new Dish { Id = 2, Name = "Dish 2" };
            var dish3 = new Dish { Id = 3, Name = "Dish 3" };
            var orderDish1 = new OrderDish { OrderId = 1, DishId = 1, Quantity = 3 };
            var orderDish2 = new OrderDish { OrderId = 1, DishId = 2, Quantity = 2 };
            var orderDish3 = new OrderDish { OrderId = 2, DishId = 2, Quantity = 1 };

            dbContext.AddRange(order1, order2, dish1, dish2, dish3, orderDish1, orderDish2, orderDish3);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await reportService.GetTopDishes();

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(2, result.DishNames.Count);
            Assert.AreEqual(2, result.TimesOrdered.Count);
            Assert.AreEqual("Dish 1", result.DishNames[0]);
            Assert.AreEqual(3, result.TimesOrdered[0]);
            Assert.AreEqual("Dish 2", result.DishNames[1]);
            Assert.AreEqual(3, result.TimesOrdered[1]);
        }

        [Test]
        public async Task TotalAmountEarned_ShouldReturnCorrectTotalAmount()
        {
            // Arrange
            var order1 = new Order { Id = 1, IsPaid = true };
            var order2 = new Order { Id = 2, IsPaid = true };
            var dish1 = new Dish { Id = 1, Price = 10.00m };
            var dish2 = new Dish { Id = 2, Price = 15.00m };
            var orderDish1 = new OrderDish { OrderId = 1, DishId = 1, Quantity = 2 };
            var orderDish2 = new OrderDish { OrderId = 1, DishId = 2, Quantity = 1 };
            var orderDish3 = new OrderDish { OrderId = 2, DishId = 2, Quantity = 1 };

            dbContext.AddRange(order1, order2, dish1, dish2, orderDish1, orderDish2, orderDish3);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await reportService.TotalAmountEarned();

            // Assert
            Assert.AreEqual(40.00m, result); // 2 * 10.00 + 1 * 15.00 = 40.00
        }

        [Test]
        public async Task GetTopTables_ShouldReturnTopTablesOrderedByOrderCount()
        {
            // Arrange
            var order1 = new Order { Id = 1, IsPaid = true, TableNumber = 1 };
            var order2 = new Order { Id = 2, IsPaid = true, TableNumber = 2 };
            var order3 = new Order { Id = 3, IsPaid = true, TableNumber = 3 };
            var table1 = new Table { Id = 1, TableName = "Table 1" };
            var table2 = new Table { Id = 2, TableName = "Table 2" };
            var table3 = new Table { Id = 3, TableName = "Table 3" };

            dbContext.AddRange(order1, order2, order3, table1, table2, table3);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await reportService.GetTopTables();

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(3, result.TableNumber.Count);
            Assert.AreEqual(3, result.OrderCount.Count);
            Assert.AreEqual("Table 1", result.TableNumber[0]);
            Assert.AreEqual(1, result.OrderCount[0]);
            Assert.AreEqual("Table 2", result.TableNumber[1]);
            Assert.AreEqual(1, result.OrderCount[1]);
            Assert.AreEqual("Table 3", result.TableNumber[2]);
            Assert.AreEqual(1, result.OrderCount[2]);
        }
        [TearDown]
        public void TearDown()
        {
            this.dbContext.Database.EnsureDeleted();
            this.dbContext.DisposeAsync();
        }
    }
}
