using KZERP.Core.Entities.Warehouses;
using KZERP.Core.Interfaces.IWarehouseRepository;
using KZERP.Core.Interfaces.IWarehousesService;

namespace KZERP.Core.Services.WarehousesService
{
    public class WarehousesService : IWarehousesService
    {
        private readonly IWarehouseRepository _warehouseRepository;
        public WarehousesService(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public async Task<List<Warehouse>> GetAllWarehousesAsync()
        {
            return await _warehouseRepository.GetAllWarehousesAsync();
        }

        public async Task<Warehouse> GetWarehouseByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID");
            }
            return await _warehouseRepository.GetWarehouseByIdAsync(id);
        }

        public async Task AddWarehouseAsync(Warehouse warehouse)
        {
            if (string.IsNullOrEmpty(warehouse.Name))
                throw new ArgumentException("Warehouse name required");
            await _warehouseRepository.AddWarehouseAsync(warehouse);
        }

        public async Task UpdateWarehouseAsync(Warehouse warehouse)
        {
            await _warehouseRepository.UpdateWarehouseAsync(warehouse);
        }

        public async Task DeleteWarehouseAsync(int id)
        {
            await _warehouseRepository.DeleteWarehouseAsync(id);
        }

    }

}