using Microsoft.AspNetCore.Mvc;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Core.Models.ViewModels;
using WaiterApplication.Extensions;

namespace WaiterApplication.Controllers
{
    public class TableController : BaseController
    {
        private readonly ITableService tableService;
        public TableController(ITableService _tableService)
        {
            tableService = _tableService;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var tables = await tableService.AllAsync();

            return View(tables);
        }
        [HttpGet]
        public async Task<IActionResult> CreateTable()
        {
            if (await tableService.TableExistsAsync(User.Id()))
            {
                return RedirectToAction("All");
            }

            var model = new TableViewModel();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTable(TableViewModel model)
        {
            if (await tableService.TableExistsAsync(User.Id()))
            {
                return RedirectToAction("All");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await tableService.CreateTableAsync(model.Name,
                model.Capacity,
                model.Status);

            return RedirectToAction("All");
        }
        [HttpGet]
        public async Task<IActionResult> RemoveTable(int tableId)
        {
            if (await tableService.TableExistsAsync(tableId.ToString()) == false)
            {
                return BadRequest();
            }

            var table = await tableService.TableDetailsByIdAsync(tableId);

            TableViewModel model = new TableViewModel()
            {
                Id = table.Id,
                Name = table.Name,
                Capacity = table.Capacity
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveTable(TableViewModel model)
        {
            if (!await tableService.TableExistsAsync(model.Id.ToString()))
            {
                return BadRequest();
            }
            await tableService.RemoveTable(model.Id);

            return RedirectToAction("All");
        }
        [HttpGet]
        public async Task<IActionResult> TableDetails(int tableId)
        {
            var table = await tableService.TableDetailsByIdAsync(tableId);

            return View(table);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int tableId)
        {
            if (await tableService.TableExistsAsync(tableId.ToString()) == false)
            {
                return BadRequest();
            }
            var tableToEdit = await tableService.TableDetailsByIdAsync(tableId);
            var model = new TableViewModel()
            {
                Id = tableToEdit.Id,
                Name = tableToEdit.Name,
                Capacity = tableToEdit.Capacity,
                Status = tableToEdit.Status
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TableViewModel model)
        {
            if (await tableService.TableExistsAsync(model.Id.ToString()) == false)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await tableService.EditAsync(model);

            return RedirectToAction("All");
        }
    }
}
