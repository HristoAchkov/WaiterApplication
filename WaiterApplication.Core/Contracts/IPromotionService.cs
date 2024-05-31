using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiterApplication.Core.Models.QueryModels;
using WaiterApplication.Core.Models.ViewModels;
using WaiterApplication.Infrastructure.Data.Models;

namespace WaiterApplication.Core.Contracts
{
    public interface IPromotionService
    {
        Task AddDishToPromo(int dishId, int promoId);
        Task<List<AllDishesServiceModel>> GetDishes();
        Task CreatePromotion(Promotion promotion);
        Task<List<AllPromotionsViewModel>> AllPromotions();
        Task<PromoDetailsServiceModel> PromoDetailsByIdAsync(int id);
        Task CreatePromoDish(int promotionId, int dishId);
        Task EditAsync(int id, PromoDetailsServiceModel model);
        Task Delete(int id);
    }
}
