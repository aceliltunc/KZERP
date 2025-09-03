using KZERP.Core.DTOs.WarehouseDTO;
using KZERP.Core.Entities.Warehouses;

namespace KZERP.Core.Interfaces.IWarehousesService
{
    public interface IWarehousesService
    {
        Task<List<Warehouse>> GetAllWarehousesAsync();
        Task<Warehouse> GetWarehouseByIdAsync(int id);
        Task AddWarehouseAsync(WarehouseDTO warehouse);
        Task UpdateWarehouseAsync(Warehouse warehouse);
        Task DeleteWarehouseAsync(int id);
    
    }
}