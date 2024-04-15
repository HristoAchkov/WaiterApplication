using WaiterApplication.Core.Enumerations;
using WaiterApplication.Core.Models.ViewModels;

namespace WaiterApplication.Core.Contracts
{
    public interface ITableService
    {
        Task<List<TableViewModel>> AllAsync(TableStatus status = TableStatus.Vacant);
        Task CreateTableAsync(string name, int capacity, bool status = false);
        Task<bool> TableExistsAsync(string tableId);
        Task RemoveTable(int id);
        Task<TableViewModel> TableDetailsByIdAsync(int id);
        Task<bool> IsTableTaken(int id);
    }
}
