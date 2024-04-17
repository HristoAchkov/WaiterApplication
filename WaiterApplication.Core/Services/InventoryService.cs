using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Core.Models.ViewModel;
using WaiterApplication.Core.Models.ViewModels;
using WaiterApplication.Infrastructure.Data.Common;
using WaiterApplication.Infrastructure.Data.Models;

namespace WaiterApplication.Core.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IRepository repository;

        public InventoryService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<List<InventoryItemViewModel>> AllInventoryItemsAsync()
        {
           var items = await repository.AllAsNoTracking<InventoryItem>()
                .Select(i => new InventoryItemViewModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    Quantity = i.Quantity,
                    UnitOfMeasurement = i.UnitOfMeasurement
                }).ToListAsync();

            return items;
        }

        public async Task CreateItem(string name, int quantity, string? unitOfMeasurement)
        {
            var item = new InventoryItem()
            {
                Name = name,
                Quantity = quantity,
                UnitOfMeasurement = unitOfMeasurement
            };
            await repository.AddAsync<InventoryItem>(item);
            await repository.SaveChangesAsync();
        }

        public async Task<InventoryItemViewModel> ItemDetailsByIdAsync(int id)
        {
            return await repository.AllAsNoTracking<InventoryItem>()
                .Where(i => i.Id == id)
                .Select(i => new InventoryItemViewModel()
                {
                    Name = i.Name,
                    Quantity = i.Quantity,
                    UnitOfMeasurement = i.UnitOfMeasurement
                })
                .FirstAsync();
        }

        public async Task<bool> ItemExistsAsync(string itemId)
        {
            return await repository.AllAsNoTracking<InventoryItem>()
                .AnyAsync(i => i.Id.ToString() == itemId);
        }
        public async Task EditAsync(int itemId, InventoryItemViewModel model)
        {
            var item = await repository.GetByIdAsync<InventoryItem>(itemId);

            if (item != null)
            {
                item.Name = model.Name;
                item.Quantity = model.Quantity;
                item.UnitOfMeasurement = model.UnitOfMeasurement;
            }
            await repository.SaveChangesAsync();
        }
    }
}
