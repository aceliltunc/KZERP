using KZERP.Core.Entities.RfidLogs;

namespace KZERP.Core.Interfaces.IRfidRepository
{
    public interface IRfidRepository
    {
        Task<List<RfidLogs>> GetAllRfidsAsync();
        Task<RfidLogs?> GetRfidByIdAsync(int id);
        Task AddRfidAsync(RfidLogs rfid);
        Task UpdateRfidAsync(RfidLogs rfid);
        Task DeleteRfidAsync(int id);

    }
    
}