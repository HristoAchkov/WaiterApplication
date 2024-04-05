using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Core.Contracts;
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

        public async Task<bool> DishExistsAsync(string dishId)
        {
            return await repository.AllAsNoTracking<Dish>()
                .AnyAsync(d => d.Id.ToString() == dishId);
        }
    }
}
