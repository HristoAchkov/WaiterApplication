using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Core.Models.ViewModels;
using WaiterApplication.Extensions;
using static WaiterApplication.Infrastructure.Constants.DataConstants;

namespace WaiterApplication.Controllers
{
    [Authorize(Roles = AdminRole)]
    public class InventoryController : BaseController
    {
        private readonly IInventoryService inventoryService;
        public InventoryController(IInventoryService _inventoryService)
        {
            inventoryService = _inventoryService;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var items = await inventoryService.AllInventoryItemsAsync();

            return View(items);
        }
        [HttpGet]
        public async Task<IActionResult> CreateItem()
        {
            if (await inventoryService.ItemExistsAsync(User.Id()))
            {
                return BadRequest();
            }

            var model = new InventoryItemViewModel();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateItem(InventoryItemViewModel model)
        {

            if (await inventoryService.ItemExistsAsync(User.Id()))
            {
                return RedirectToAction("All");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await inventoryService.CreateItem(model.Name,
                model.Quantity,
                model.UnitOfMeasurement);
            return RedirectToAction("All");
        }
    }
}
