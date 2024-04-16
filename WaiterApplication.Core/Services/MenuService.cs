using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Core.Enumerations;
using WaiterApplication.Core.Models.QueryModels;
using WaiterApplication.Core.Models.ViewModel;
using WaiterApplication.Core.Models.ViewModels;
using WaiterApplication.Infrastructure.Data.Common;
using WaiterApplication.Infrastructure.Data.Models;

namespace WaiterApplication.Core.Services
{
    public class MenuService : IMenuService
    {
        private readonly IRepository repository;

        public MenuService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task AddDishAsync(string name, string description, string imageUrl, decimal price, string? ingredients)
        {
            var dish = new Dish()
            {
                Name = name,
                Description = description,
                Image = imageUrl,
                Price = price,
                Ingredients = ingredients
            };

            await repository.AddAsync(dish);
            await repository.SaveChangesAsync();
        }

        public async Task<AllDishesQueryModel> AllAsync(
           string? searchTerm = null,
           DishSorting sorting = DishSorting.Name,
           int currentPage = 1,
           int housesPerPage = 1)
        {
            var dishesToShow = repository.AllAsNoTracking<Dish>();

            if (searchTerm != null)
            {
                string normalizedSearchTerm = searchTerm.ToLower();
                dishesToShow = dishesToShow
                    .Where(h => (h.Name.ToLower().Contains(normalizedSearchTerm)));
            }

            dishesToShow = sorting switch
            {
                DishSorting.Name => dishesToShow
                    .OrderBy(h => h.Name),
                DishSorting.Price => dishesToShow
                    .OrderBy(h => h.Price),
                _ => dishesToShow
                    .OrderByDescending(h => h.Id)
            };

            var dishes = await dishesToShow
                .Skip((currentPage - 1) * housesPerPage)
                .Take(housesPerPage)
                .Select(d => new AllDishesServiceModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    Price = d.Price,
                    Image = d.Image,
                    Ingredients = d.Ingredients
                })
                .ToListAsync();

            int totalDishes = await dishesToShow.CountAsync();

            return new AllDishesQueryModel()
            {
                Dishes = dishes,
                TotalDishesCount = totalDishes
            };
        }

        public async Task DeleteAsync(int id)
        {
            await repository.RemoveAsync<Dish>(id);
            await repository.SaveChangesAsync();
        }

        public async Task<DishDetailsServiceModel> DishDetailsByIdAsync(int id)
        {
            return await repository.AllAsNoTracking<Dish>()
                .Where(d => d.Id == id)
                .Select(d => new DishDetailsServiceModel()
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    Image = d.Image,
                    Price = d.Price,
                    Ingredients = d.Ingredients
                })
                .FirstAsync();
        }

        public async Task<bool> DishExistsAsync(string dishId)
        {
            return await repository.AllAsNoTracking<Dish>()
                .AnyAsync(d => d.Id.ToString() == dishId);
        }

        public async Task EditAsync(int dishId, DishFormModel model)
        {
            var dish = await repository.GetByIdAsync<Dish>(dishId);

            if (dish != null)
            {
                dish.Name = model.Name;
                dish.Image = model.Image;
                dish.Price = model.Price;
                dish.Ingredients = model.Ingredients;
            }
            await repository.SaveChangesAsync();
        }

        public async Task<List<AllDishesServiceModel>> TableAllAsync()
        {
            var dishesToShow = await repository.AllAsNoTracking<Dish>()
                .Select(x => new AllDishesServiceModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Image = x.Image
                }).ToListAsync();

            return dishesToShow;
        }
    }
}
