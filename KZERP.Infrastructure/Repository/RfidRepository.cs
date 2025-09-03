using KZERP.Core.Entities.RfidLogs;
using KZERP.Core.Interfaces.IRfidRepository;
using KZERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace KZERP.Infrastructure.Repository.RfidRepository
{
    public class RfidRepository : IRfidRepository
    {
        private readonly KZERPDbContext _context;

        public RfidRepository(KZERPDbContext context)
        {
            _context = context;
        }

        public async Task<List<RfidLogs>> GetAllRfidsAsync()
        {
            return await _context.RfidLogs.ToListAsync();
        }


        public async Task<RfidLogs?> GetRfidByIdAsync(int id)
        {
            return await _context.RfidLogs.FindAsync(id);

        }

        public async Task AddRfidAsync(RfidLogs rfid)
        {
            await _context.RfidLogs.AddAsync(rfid);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRfidAsync(RfidLogs rfid)
        {
            _context.RfidLogs.Update(rfid);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRfidAsync(int id)
        {
            var rfid = await GetRfidByIdAsync(id);
            if (rfid != null)
            {
                _context.RfidLogs.Remove(rfid);
                await _context.SaveChangesAsync();
            }
        }
    }
}