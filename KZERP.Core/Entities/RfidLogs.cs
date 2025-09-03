using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace KZERP.Core.Entities.RfidLogs
{
    public class RfidLogs
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? CardUID { get; set; }
        public string? DataContent { get; set; }
        public DateTime ScanTime { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Turkey Standard Time");


    }
}