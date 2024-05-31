using Microsoft.AspNetCore.Mvc;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Core.Models.ViewModels;
using WaiterApplication.Infrastructure.Data.Models;
using WaiterApplication.Core.Models.QueryModels;

namespace WaiterApplication.Controllers
{
    public class PromotionController : BaseAdminController
    {
        private readonly IPromotionService promotionService;
        private readonly IMenuService menuService;
        public PromotionController(IPromotionService _promotionService,
            IMenuService _menuService)
        {
            promotionService = _promotionService;
            menuService = _menuService;
        }
        [HttpGet]
        public async Task<IActionResult> AllDishes()
        {
            var model = await promotionService.GetDishes();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreatePromotion(int dishId)
        {
            var promotion = new PromotionViewModel()
            {
                Name = "Promotion",
                Price = 1,
                Dishes = new List<AllDishesServiceModel>()
            };

            Promotion promo = new Promotion()
            {
                Name = promotion.Name,
                Price = promotion.Price
            };

            var currentDish = await menuService.GetDishDetailsByIdAsync(dishId);

            var promoDish = new PromoDish()
            {
                DishId = currentDish.Id,
                PromotionId = promo.Id,
            };
            
            promo.Dishes.Add(promoDish);

            promotionService.CreatePromotion(promo);

            return View(promo);
        }
        [HttpGet]
        public async Task<IActionResult> AllPromotions(List<AllPromotionsViewModel> model)
        {
            model = await promotionService.AllPromotions();

            foreach (var promotion in model)
            {
                foreach (var promoDish in promotion.Dishes)
                {
                    var dish = await menuService.GetDishDetailsByIdAsync(promoDish.DishId);
                    if (dish != null)
                    {
                        promoDish.Dish = dish;
                    }
                }
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditPromo(int id)
        {
            var promo = await promotionService.PromoDetailsByIdAsync(id);

            var model = new PromoDetailsServiceModel()
            {
                Id = promo.Id,
                Name = promo.Name,
                Price = promo.Price,
                Dishes = promo.Dishes,
            };
            foreach (var promoDish in model.Dishes)
            {
                var dish = await menuService.GetDishDetailsByIdAsync(promoDish.DishId);
                if (dish != null)
                {
                    promoDish.Dish = dish;
                }
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditPromo(int id, PromoDetailsServiceModel model)
        {
            await promotionService.EditAsync(id, model);

            return RedirectToAction(nameof(AllPromotions));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var promo = await promotionService.PromoDetailsByIdAsync(id);

            var model = new PromoDetailsServiceModel()
            {
                Id = promo.Id,
                Name = promo.Name,
                Price = promo.Price,
                Dishes = promo.Dishes
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(PromoDetailsServiceModel model)
        {
            if (model.Dishes != null)
            {
                var id = model.Dishes.Select(x => x.Id).First();
                await menuService.DeleteAsync(id);
            }

            await promotionService.Delete(model.Id);

            return RedirectToAction(nameof(AllPromotions));
        }
        [HttpGet]
        public async Task<IActionResult> AddAnotherDish(int promotionId)
        {
            var allDishes = await promotionService.GetDishes();

            ViewBag.PromotionId = promotionId;

            return View(allDishes);
        }
        [HttpGet]
        public async Task<IActionResult> AddAnotherDishPost(int dishId, int promotionId)
        {
            var promoDish = new PromoDish()
            {
                DishId = dishId,
                PromotionId = promotionId,
            };

            await promotionService.AddPromoDishToPromo(dishId, promoDish.PromotionId);

            return RedirectToAction(nameof(AllPromotions));
        }
    }
}
