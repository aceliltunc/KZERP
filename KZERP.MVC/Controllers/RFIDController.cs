using System.Text.Json;
using KZERP.Core.DTOs.RfidDTO;
using KZERP.Core.Entities.RfidLogs;
using KZERP.Core.Interfaces.IRfidService;
using KZERP.Core.Services.RfidService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KZERP.MVC.Controllers
{
    public class RFIDController : Controller
    {
        private readonly IRfidService _rfidService;

        public RFIDController(IRfidService rfidService)
        {
            _rfidService = rfidService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var rfids = await _rfidService.GetAllRfidsAsync();

            return View(rfids);
        }


        public async Task<IActionResult> Details(int id)
        {
            var rfid = await _rfidService.GetRfidByIdAsync(id);
            if (rfid == null) return NotFound();
            return View(rfid);
        }


        // add new rfid (GET)
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // add new rfid (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(RfidDTO rfid)
        {

            if (ModelState.IsValid)
            {
                await _rfidService.AddRfidAsync(rfid);
                return RedirectToAction(nameof(Index));
            }
            return View(rfid);
        }

        // Update (GET)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var rfid = await _rfidService.GetRfidByIdAsync(id);
            if (rfid == null) return NotFound();
            return View(rfid);
        }

        // Update (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(RfidLogs rfid)
        {
            if (ModelState.IsValid)
            {
                await _rfidService.UpdateRfidAsync(rfid);
                return RedirectToAction(nameof(Index));
            }
            return View(rfid);
        }

        // Delete
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var rfid = await _rfidService.GetRfidByIdAsync(id);
            if (rfid == null) return NotFound();
            return View(rfid);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _rfidService.DeleteRfidAsync(id);
            return RedirectToAction("Index");
        }
    }
}