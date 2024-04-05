﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WaiterApplication.Attributes;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Core.Models;
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
                return RedirectToAction("Index", nameof(MenuController));
            }

            var model = new CreateDishModel();

            return View(model);
        }

        [HttpPost]
        [DishNotInTheMenu]
        public async Task<IActionResult> AddDishToMenu(CreateDishModel model)
        {
            if (await menuService.DishExistsAsync(User.Id()))
            {
                return RedirectToAction("Index", nameof(MenuController));
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

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
