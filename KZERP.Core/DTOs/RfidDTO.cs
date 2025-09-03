using System.ComponentModel.DataAnnotations;

namespace KZERP.Core.DTOs.RfidDTO
{
    public class RfidDTO
    {
        [Required]
        public string? CardUID { get; set; }
        public string? DataContent { get; set; }
        public DateTime ScanTime { get; set; } = DateTime.UtcNow.AddHours(3);

    }
}