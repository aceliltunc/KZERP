using System.ComponentModel.DataAnnotations;
using KZERP.Core.Entities.Products;

namespace KZERP.Core.Entities.WorkOrders
{
    public class WorkOrder
    {
        public int Id { get; set; }
        [Required]
        Product? ProductId { get; set; }
        public int PlannedQuantity { get; set; }
        public int PlannedTimeMin { get; set; }
        public DateTime ScheduledStart { get; set; }
        public DateTime ScheduledEnd { get; set; }
        public string? Status { get; set; } // Planned, InProgress, Completed, Cancelled, Pending
    }
}