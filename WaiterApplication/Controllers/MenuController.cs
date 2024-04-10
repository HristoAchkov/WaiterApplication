using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WaiterApplication.Attributes;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Core.Models;
using WaiterApplication.Core.Models.QueryModels;
using WaiterApplication.Core.Models.ViewModel;
using WaiterApplication.Core.Models.ViewModels;
using WaiterApplication.Core.Services;
using WaiterApplication.Extensions;

namespace WaiterApplication.Controllers
{
    public class MenuController : BaseController
    {
        private readonly IMenuService menuService;
        public MenuController(IMenuService _menuService)
        {
            menuService = _menuService;
        }

        [HttpGet]
        [DishNotInTheMenu]
        //Admin only
        public async Task<IActionResult> AddDishToMenu()
        {
            if (await menuService.DishExistsAsync(User.Id()))
            {
                return RedirectToAction("Index",nameof(HomeController));
            }

            var model = new DishFormModel();

            return View(model);
        }

        [HttpPost]
        [DishNotInTheMenu]
        public async Task<IActionResult> AddDishToMenu(DishFormModel model)
        {
            if (await menuService.DishExistsAsync(User.Id()))
            {
                return RedirectToAction("Index",nameof(HomeController));
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await menuService.AddDishAsync(model.Name,
                model.Description,
                model.Image,
                model.Price,
                model.Ingredients);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> All([FromQuery] AllDishesQueryModel model)
        {
            var dishes = await menuService.AllAsync(
                model.SearchTerm,
                model.Sorting,
                model.CurrentPage,
                model.DishesPerPage);

            model.TotalDishesCount = dishes.TotalDishesCount;
            model.Dishes = dishes.Dishes;

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (await menuService.DishExistsAsync(id.ToString()) == false)
            {
                return BadRequest();
            }

            var model = await menuService.DishDetailsByIdAsync(id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await menuService.DishExistsAsync(id.ToString()) == false)
            {
                return BadRequest();
            }

            var dish = await menuService.DishDetailsByIdAsync(id);
            var model = new DishFormModel()
            {
                Name = dish.Name,
                Description = dish.Description,
                Image = dish.Image,
                Price = dish.Price,
                Ingredients = dish.Ingredients
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,DishFormModel model)
        {
            if (await menuService.DishExistsAsync(id.ToString()) == false)
            {
                return BadRequest();
            }
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await menuService.EditAsync(id, model);

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (await menuService.DishExistsAsync(id.ToString()) == false)
            {
                return BadRequest();
            }

            var dish = await menuService.DishDetailsByIdAsync(id);
            var model = new DishRemoveViewModel()
            {
                Id = dish.Id,
                Name = dish.Name,
                Image = dish.Image,
                Price = dish.Price
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(DishRemoveViewModel model, int id)
        {
            if (await menuService.DishExistsAsync(model.Id.ToString()) == false)
            {
                return BadRequest();
            }

            await menuService.DeleteAsync(model.Id);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
