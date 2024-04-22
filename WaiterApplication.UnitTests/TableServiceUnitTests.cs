using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Infrastructure.Data.Common;
using WaiterApplication.Infrastructure.Data;
using WaiterApplication.Infrastructure.Data.Models;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using WaiterApplication.Core.Services;
using WaiterApplication.Core.Enumerations;
using WaiterApplication.Core.Models.ViewModels;

namespace WaiterApplication.UnitTests
{
    internal class TableServiceUnitTests
    {
        private WaiterApplicationDbContext dbContext;
        private IRepository repository;
        private ITableService tableService;

        private IEnumerable<Table> tables;

        private Table tableOne;
        private Table tableTwo;
        private Table tableThree;
        private Table tableFour;

        [SetUp]
        public void Setup()
        {
            Table tableOne = new Table
            {
                Id = 1,
                TableName = "Table One",
                Capacity = 4,
                Status = true 
            };

            Table tableTwo = new Table
            {
                Id = 2,
                TableName = "Table Two",
                Capacity = 6,
                Status = false 
            };

            Table tableThree = new Table
            {
                Id = 3,
                TableName = "Table Three",
                Capacity = 2,
                Status = true 
            };

            Table tableFour = new Table
            {
                Id = 4,
                TableName = "Table Four",
                Capacity = 8,
                Status = false 
            };

            tables = new List<Table> { tableOne, tableTwo, tableThree, tableFour };

            var options = new DbContextOptionsBuilder<WaiterApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            dbContext = new WaiterApplicationDbContext(options);
            dbContext.AddRangeAsync(tables);
            dbContext.SaveChanges();

            repository = new Repository(dbContext);
            tableService = new TableService(repository);
        }
        [Test]
        public async Task CreateTableAsync_CreatesNewTable()
        {
            var newTableId = 5;
            var newTableName = "Test Table";
            var newTableCapacity = 4;
            var newTableStatus = false;

            await tableService.CreateTableAsync(newTableName, newTableCapacity, newTableStatus);

            var createdTable = await tableService.TableDetailsByIdAsync(newTableId);
            Assert.IsNotNull(createdTable);
            Assert.AreEqual(newTableName, createdTable.Name);
            Assert.AreEqual(newTableCapacity, createdTable.Capacity);
            Assert.AreEqual(newTableStatus, createdTable.Status);
        }

        [Test]
        public async Task RemoveTable_RemovesExistingTable()
        {
            var existingTableId = tables.Select(x => x.Id).First();

            await tableService.RemoveTable(existingTableId);

            var tableExists = await tableService.TableExistsAsync(existingTableId.ToString());
            Assert.IsFalse(tableExists);
        }

        [Test]
        public async Task TableExistsAsync_ReturnsTrueForExistingTable()
        {
            var existingTableId = tables.Select(x => x.Id).First();

            var tableExists = await tableService.TableExistsAsync(existingTableId.ToString());

            Assert.IsTrue(tableExists);
        }

        [Test]
        public async Task TableExistsAsync_ReturnsFalseForNonExistingTable()
        {
            var nonExistingTableId = 999;

            var tableExists = await tableService.TableExistsAsync(nonExistingTableId.ToString());

            Assert.IsFalse(tableExists);
        }

        [Test]
        public async Task IsTableTaken_ReturnsCorrectStatus()
        {
            var existingTableId = tables.Select(x => x.Id).First();
            var expectedStatus = tables.Select(x => x.Status).First();

            var isTableTaken = await tableService.IsTableTaken(existingTableId);

            Assert.AreEqual(expectedStatus, isTableTaken);
        }

        [Test]
        public async Task EditAsync_UpdatesTableDetails()
        {
            var existingTableId = tables.Select(x => x.Id).First();
            var updatedTableDetails = new TableViewModel
            {
                Id = existingTableId,
                Name = "Updated Table Name",
                Capacity = 6,
                Status = false
            };

            await tableService.EditAsync(updatedTableDetails);

            var editedTable = await tableService.TableDetailsByIdAsync(existingTableId);
            Assert.IsNotNull(editedTable);
            Assert.AreEqual(updatedTableDetails.Name, editedTable.Name);
            Assert.AreEqual(updatedTableDetails.Capacity, editedTable.Capacity);
            Assert.AreEqual(updatedTableDetails.Status, editedTable.Status);
        }

        [TearDown]
        public void TearDown()
        {
            this.dbContext.Database.EnsureDeleted();
            this.dbContext.DisposeAsync();
        }
    }
}
