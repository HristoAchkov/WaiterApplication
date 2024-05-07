using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Core.Enumerations;
using WaiterApplication.Core.Models.ViewModels;
using WaiterApplication.Infrastructure.Data.Common;
using WaiterApplication.Infrastructure.Data.Models;

namespace WaiterApplication.Core.Services
{
    public class TableService : ITableService
    {
        private readonly IRepository repository;

        public TableService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<List<TableViewModel>> AllAsync(TableStatus status = TableStatus.Vacant)
        {
            var tablesToShow = await repository.AllAsNoTracking<Table>()
                .Select(t => new TableViewModel
                {
                    Id = t.Id,
                    Name = t.TableName,
                    Capacity = t.Capacity,
                    Status = t.Status
                })
                .ToListAsync();

            return tablesToShow;
        }

        public async Task CreateTableAsync(string name, int capacity, bool status)
        {
            var table = new Table()
            {
                TableName = name,
                Capacity = capacity,
                Status = status
            };

            await repository.AddAsync<Table>(table);
            await repository.SaveChangesAsync();
        }

        public async Task<TableViewModel> TableDetailsByIdAsync(int id)
        {
            return await repository.All<Table>()
                .Where(d => d.Id == id)
                .Select(d => new TableViewModel()
                {
                    Id = d.Id,
                    Name = d.TableName,
                    Capacity = d.Capacity,
                    Status = d.Status
                })
                .FirstAsync();
        }

        public async Task RemoveTable(int tableId)
        {
            await repository.RemoveAsync<Table>(tableId);
            await repository.SaveChangesAsync();
        }

        public async Task<bool> TableExistsAsync(string tableId)
        {
            return await repository.AllAsNoTracking<Table>()
                .AnyAsync(t => t.Id.ToString() == tableId);
        }

        public async Task<bool> IsTableTaken(int id)
        {
            var table = await repository.GetByIdAsync<Table>(id);

            return table.Status;
        }

        public async Task EditAsync(TableViewModel model)
        {
            var table = await repository.GetByIdAsync<Table>(model.Id);

            if (table != null)
            {
                table.TableName = model.Name;
                table.Capacity = model.Capacity;
                table.Status = model.Status;
            }
            await repository.SaveChangesAsync();
        }
    }
}
