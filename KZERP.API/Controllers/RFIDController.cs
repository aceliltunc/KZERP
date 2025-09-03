using KZERP.Core.Entities.RfidLogs;
using KZERP.Core.Interfaces.IRfidService;
using Microsoft.AspNetCore.Mvc;

namespace KZERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RFIDController : ControllerBase
    {
        private readonly IRfidService _rfidService;
        public RFIDController(IRfidService rfidService)
        {
            _rfidService = rfidService;
        }

        // GET: api/Rfid
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RfidLogs>>> GetRfids()
        {
            var rfids = await _rfidService.GetAllRfidsAsync();
            return Ok(rfids);
        }

        // GET: api/Rfid/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RfidLogs>> GetRfid(int id)
        {
            var rfid = await _rfidService.GetRfidByIdAsync(id);
            if (rfid == null)
            {
                return NotFound();
            }
            return Ok(rfid);
        }

        // POST: api/Rfid
        [HttpPost]
        public async Task<ActionResult<RfidLogs>> PostRfid(RfidLogs rfid)
        {
            await _rfidService.AddRfidAsync(rfid);
            return Ok(rfid);
        }

        // PUT: api/Rfid/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRfid(int id, RfidLogs rfid)
        {
            if (id != rfid.Id)
            {
                return BadRequest();
            }

            try
            {
                await _rfidService.UpdateRfidAsync(rfid);
            }
            catch (Exception)
            {
                
                throw;
            }

            return NoContent();
        }


        // DELETE: api/Rfid/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRfid(int id)
        {
            await _rfidService.DeleteRfidAsync(id);
            return NoContent();
        }


    }
}