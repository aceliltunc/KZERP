using KZERP.Core.DTOs.RfidDTO;
using KZERP.Core.Entities.RfidLogs;

namespace KZERP.Core.Interfaces.IRfidService
{
    public interface IRfidService
    {
        Task<List<RfidLogs>> GetAllRfidsAsync();
        Task<RfidLogs?> GetRfidByIdAsync(int id);
        Task AddRfidAsync(RfidDTO rfidDto);
        Task UpdateRfidAsync(RfidLogs rfid);
        Task DeleteRfidAsync(int id);
    }
    
}