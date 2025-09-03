using KZERP.Core.Entities.Warehouses;
using KZERP.Core.Interfaces.IWarehouseRepository;
using KZERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace KZERP.Infrastructure.Repository.WarehousesRepository
{
    public class WarehousesRepository : IWarehouseRepository
    {
        private readonly KZERPDbContext _context;

        public WarehousesRepository(KZERPDbContext context)
        {
            _context = context;
        }

        public async Task<List<Warehouse>> GetAllWarehousesAsync()
        {
            return await _context.Warehouses.ToListAsync();
        }


        public async Task<Warehouse> GetWarehouseByIdAsync(int id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);

            if (warehouse == null)
            {
                throw new NullReferenceException($"Warehouse with ID {id} not found");
            }
            return warehouse;
        }


        public async Task AddWarehouseAsync(Warehouse warehouse)
        {
            await _context.Warehouses.AddAsync(warehouse);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWarehouseAsync(Warehouse warehouse)
        {
            _context.Warehouses.Update(warehouse);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteWarehouseAsync(int id)
        {
            var warehouse = await GetWarehouseByIdAsync(id);
            if (warehouse != null)
            {
                _context.Warehouses.Remove(warehouse);
                await _context.SaveChangesAsync();
            }


        }

    }
}