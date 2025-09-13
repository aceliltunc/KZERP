


using KZERP.Core.DTOs.WarehouseDTO;
using KZERP.Core.Entities.Warehouses;
using KZERP.Core.Interfaces.IWarehousesService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KZERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WarehousesController : ControllerBase
    {
        private readonly IWarehousesService _warehousesService;
        public WarehousesController(IWarehousesService warehousesService)
        {
            _warehousesService = warehousesService;
        }

        //Get: api/Warehouses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Warehouse>>> GetWarehouses()
        {
            var warehouse = await _warehousesService.GetAllWarehousesAsync();
            return Ok(warehouse);
        }

        //Get: api/Warehouses/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Warehouse>> GetWarehouseById(int id)
        {
            var warehouse = await _warehousesService.GetWarehouseByIdAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }
            return Ok(warehouse);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult> PostWarehouse(WarehouseDTO warehouseDTO)
        {
            await _warehousesService.AddWarehouseAsync(warehouseDTO);
            return Ok(warehouseDTO);
        }

        // PUT: api/Products
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouse(int id, Warehouse warehouse)
        {
            if (id != warehouse.Id)
            {
                return BadRequest();
            }

            try
            {
                await _warehousesService.UpdateWarehouseAsync(warehouse);
            }
            catch (Exception)
            {
                throw;
            }

            return NoContent();
        }



        // DELETE: api/Products
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            await _warehousesService.DeleteWarehouseAsync(id);
            return NoContent();
        }



    }
}

