using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Core.Models.ViewModels;

namespace WaiterApplication.Core.Contracts
{
    public interface IInventoryService
    {
        Task CreateItem(string name, int quantity, string? unitOfMeasurement);
        Task<List<InventoryItemViewModel>> AllInventoryItemsAsync();
        Task<bool> ItemExistsAsync(string itemId);
        Task<InventoryItemViewModel> ItemDetailsByIdAsync(int id);
        Task EditAsync(int itemId, InventoryItemViewModel model);
    }
}
