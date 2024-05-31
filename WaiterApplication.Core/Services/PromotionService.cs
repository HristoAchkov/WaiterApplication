using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Core.Models.QueryModels;
using WaiterApplication.Core.Models.ViewModels;
using WaiterApplication.Infrastructure.Data.Common;
using WaiterApplication.Infrastructure.Data.Models;

namespace WaiterApplication.Core.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IRepository repository;
        public PromotionService(IRepository _repository)
        {
            repository = _repository;
        }
        public async Task<List<AllDishesServiceModel>> GetDishes()
        {
            var dishes = await repository.AllAsNoTracking<Dish>()
                .Select(x => new AllDishesServiceModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    Price = x.Price
                })
                .ToListAsync();

            return dishes;
        }
        public async Task CreatePromotion(Promotion promotion)
        {
            await repository.AddAsync<Promotion>(promotion);
            await repository.SaveChangesAsync();
        }
        public async Task CreatePromoDish(int promotionId, int dishId)
        {
            var promoDish = new PromoDish()
            {
                DishId = dishId,
                PromotionId = promotionId
            };

            await repository.AddAsync<PromoDish>(promoDish);
            await repository.SaveChangesAsync();
        }

        public async Task AddDishToPromo(int dishId, int promoId)
        {
            var dish = await repository.GetByIdAsync<PromoDish>(dishId);

            var promo = await repository.GetByIdAsync<Promotion>(promoId);

            promo.Dishes.Add(dish);

            await repository.SaveChangesAsync();
        }
        public async Task AddPromoDishToPromo(int dishId, int promoId)
        {
            var promo = await repository.GetByIdAsync<Promotion>(promoId);

            var promoDish = new PromoDish()
            {
                DishId = dishId,
                PromotionId = promoId
            };

            repository.AddAsync<PromoDish>(promoDish);

            promo.Dishes.Add(promoDish);

            await repository.SaveChangesAsync();
        }

        public async Task<List<AllPromotionsViewModel>> AllPromotions()
        {
            var allPromos = await repository.AllAsNoTracking<Promotion>()
                .Select(x => new AllPromotionsViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Dishes = x.Dishes
                }).ToListAsync();

            var dishes = allPromos.Select(x => x.Dishes);

            return allPromos;
        }

        public async Task<PromoDetailsServiceModel> PromoDetailsByIdAsync(int id)
        {
            return await repository.AllAsNoTracking<Promotion>()
                .Where(p => p.Id == id)
                .Select(p => new PromoDetailsServiceModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Dishes = p.Dishes
                }).FirstAsync();
        }

        public async Task EditAsync(int id, PromoDetailsServiceModel model)
        {
            var promo = await repository.GetByIdAsync<Promotion>(id);

            if (promo != null)
            {
                promo.Id = model.Id;
                promo.Name = model.Name;
                promo.Price = model.Price;
                promo.Dishes = model.Dishes;
            }
            await repository.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await repository.RemoveAsync<Promotion>(id);
            await repository.SaveChangesAsync();
        }
    }
}
