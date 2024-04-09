using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Core.Enumerations;
using WaiterApplication.Core.Models.QueryModels;
using WaiterApplication.Core.Models.ViewModels;
using WaiterApplication.Infrastructure.Data.Common;

namespace WaiterApplication.Core.Contracts
{
    public interface IMenuService
    {
        Task<bool> DishExistsAsync(string dishId);
        Task AddDishAsync(string name, string description, string? imageUrl, decimal price, string? ingredients);
        Task<AllDishesQueryModel> AllAsync(
             string? searchTerm = null,
             DishSorting sorting = DishSorting.Name,
             int currentPage = 1,
             int housesPerPage = 1);
        Task<DishDetailsServiceModel> DishDetailsByIdAsync(int id);
    }
}
