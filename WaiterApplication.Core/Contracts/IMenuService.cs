using WaiterApplication.Core.Enumerations;
using WaiterApplication.Core.Models.QueryModels;
using WaiterApplication.Core.Models.ViewModel;
using WaiterApplication.Core.Models.ViewModels;

namespace WaiterApplication.Core.Contracts
{
    public interface IMenuService
    {
        Task<bool> DishExistsAsync(string dishId);
        Task AddDishAsync(string name, string description, string imageUrl, decimal price, string? ingredients);
        Task<AllDishesQueryModel> AllAsync(
             string? searchTerm = null,
             DishSorting sorting = DishSorting.Name,
             int currentPage = 1,
             int housesPerPage = 1);
        Task<List<AllDishesServiceModel>> TableAllAsync();
        Task<DishDetailsServiceModel> DishDetailsByIdAsync(int id);
        Task EditAsync(int dishId, DishFormModel model);
        Task DeleteAsync(int id);
    }
}
