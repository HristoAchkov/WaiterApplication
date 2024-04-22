using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Infrastructure.Data.Common;
using WaiterApplication.Infrastructure.Data.Models;
using WaiterApplication.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using WaiterApplication.Core.Services;
using WaiterApplication.Core.Models.ViewModels;

namespace WaiterApplication.UnitTests
{
    [TestFixture]
    internal class InventoryServiceUnitTests
    {

        private WaiterApplicationDbContext dbContext;
        private IRepository repository;
        private IInventoryService inventoryService;

        private IEnumerable<InventoryItem> items;

        private InventoryItem itemOne;
        private InventoryItem itemTwo;
        private InventoryItem itemThree;
        private InventoryItem itemFour;

        [SetUp]
        public void Setup()
        {
            InventoryItem itemOne = new InventoryItem
            {
                Id = 1,
                Name = "Item One",
                Quantity = 10,
                UnitOfMeasurement = "kg"
            };

            InventoryItem itemTwo = new InventoryItem
            {
                Id = 2,
                Name = "Item Two",
                Quantity = 20,
                UnitOfMeasurement = "pcs"
            };

            InventoryItem itemThree = new InventoryItem
            {
                Id = 3,
                Name = "Item Three",
                Quantity = 5,
                UnitOfMeasurement = "l"
            };

            InventoryItem itemFour = new InventoryItem
            {
                Id = 4,
                Name = "Item Four",
                Quantity = 15,
                UnitOfMeasurement = "mg"
            };


            items = new List<InventoryItem>() { itemOne, itemTwo, itemThree, itemFour };

            var options = new DbContextOptionsBuilder<WaiterApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            dbContext = new WaiterApplicationDbContext(options);
            dbContext.AddRangeAsync(items);
            dbContext.SaveChanges();

            repository = new Repository(dbContext);
            inventoryService = new InventoryService(repository);
        }
        [Test]
        public async Task AllInventoryItemsAsync_ReturnsListOfItems()
        {
            var items = await inventoryService.AllInventoryItemsAsync();

            Assert.IsNotNull(items);
            Assert.IsInstanceOf<List<InventoryItemViewModel>>(items);
        }

        [Test]
        public async Task ItemDetailsByIdAsync_ReturnsCorrectItem()
        {
            var existingItemId = 1;

            var itemDetails = await inventoryService.ItemDetailsByIdAsync(existingItemId);

            Assert.IsNotNull(itemDetails);
        }

        [Test]
        public async Task ItemExistsAsync_ReturnsTrueForExistingItem()
        {
            var existingItemId = 1;

            var itemExists = await inventoryService.ItemExistsAsync(existingItemId.ToString());

            Assert.IsTrue(itemExists);
        }

        [Test]
        public async Task ItemExistsAsync_ReturnsFalseForNonExistingItem()
        {
            var nonExistingItemId = 999;

            var itemExists = await inventoryService.ItemExistsAsync(nonExistingItemId.ToString());

            Assert.IsFalse(itemExists);
        }

        [Test]
        public async Task EditAsync_UpdatesItemDetails()
        {
            var existingItemId = 1;
            var updatedItemDetails = new InventoryItemViewModel
            {
                Name = "Updated Item Name",
                Quantity = 20,
                UnitOfMeasurement = "kg"
            };

            await inventoryService.EditAsync(existingItemId, updatedItemDetails);

            var editedItem = await inventoryService.ItemDetailsByIdAsync(existingItemId);
            Assert.IsNotNull(editedItem);
            Assert.AreEqual(updatedItemDetails.Name, editedItem.Name);
            Assert.AreEqual(updatedItemDetails.Quantity, editedItem.Quantity);
            Assert.AreEqual(updatedItemDetails.UnitOfMeasurement, editedItem.UnitOfMeasurement);
        }
        [TearDown]
        public void TearDown()
        {
            this.dbContext.Database.EnsureDeleted();
            this.dbContext.DisposeAsync();
        }
    }
}
