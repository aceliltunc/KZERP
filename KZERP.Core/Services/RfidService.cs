using KZERP.Core.Entities.RfidLogs;
using KZERP.Core.Interfaces.IRfidRepository;
using KZERP.Core.Interfaces.IRfidService;

namespace KZERP.Core.Services.RfidService
{
    public class RfidService : IRfidService
    {
        private readonly IRfidRepository _rfidRepository;

        public RfidService(IRfidRepository rfidRepository)
        {
            _rfidRepository = rfidRepository;
        }

        public async Task<List<RfidLogs>> GetAllRfidsAsync()
        {
            return await _rfidRepository.GetAllRfidsAsync();
        }

        public async Task<RfidLogs?> GetRfidByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID");

            return await _rfidRepository.GetRfidByIdAsync(id);
        }

        public async Task AddRfidAsync(RfidLogs rfid)
        {
            if (string.IsNullOrEmpty(rfid.CardUID))
                throw new ArgumentException("UID required");

            await _rfidRepository.AddRfidAsync(rfid);
        }

        public async Task UpdateRfidAsync(RfidLogs product)
        {
            await _rfidRepository.UpdateRfidAsync(product);
        }

        public async Task DeleteRfidAsync(int id)
        {
            await _rfidRepository.DeleteRfidAsync(id);
        }
    }
    
}