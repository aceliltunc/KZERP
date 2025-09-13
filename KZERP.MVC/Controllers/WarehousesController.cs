


using System.Text.Json;
using KZERP.Core.DTOs.WarehouseDTO;
using KZERP.Core.Entities.Warehouses;
using KZERP.Core.Interfaces.IWarehousesService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KZERP.MVC.Controllers
{
    public class WarehousesController : Controller
    {
        private readonly IWarehousesService _warehouseService;

        public WarehousesController(IWarehousesService warehousesService)
        {
            _warehouseService = warehousesService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var warehouses = await _warehouseService.GetAllWarehousesAsync();

            return View(warehouses);
        }


        public async Task<IActionResult> Details(int id)
        {
            var warehouse = await _warehouseService.GetWarehouseByIdAsync(id);
            if (warehouse == null) return NotFound();
            return View(warehouse);
        }


        // add new warehouse (GET)
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // add new warehouse (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(WarehouseDTO warehouse)
        {

            if (ModelState.IsValid)
            {
                await _warehouseService.AddWarehouseAsync(warehouse);
                return RedirectToAction(nameof(Index));
            }
            return View(warehouse);
        }

        // Update (GET)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var warehouse = await _warehouseService.GetWarehouseByIdAsync(id);
            if (warehouse == null) return NotFound();
            return View(warehouse);
        }

        // Update (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                await _warehouseService.UpdateWarehouseAsync(warehouse);
                return RedirectToAction(nameof(Index));
            }
            return View(warehouse);
        }

        // Delete
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var warehouse = await _warehouseService.GetWarehouseByIdAsync(id);
            if (warehouse == null) return NotFound();
            return View(warehouse);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _warehouseService.DeleteWarehouseAsync(id);
            return RedirectToAction("Index");
        }
    }
}