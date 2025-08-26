using System.ComponentModel.DataAnnotations;

namespace KZERP.Core.Entities;

public class Product
{
    public int ProductId { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Category { get; set; }
    public bool IsActive { get; set; } = true;
}

public class Warehouse
{
    public int WarehouseId { get; set; }
    public string? Name { get; set; }
    public string? Location { get; set; }
}

public class InventoryStock
{
    [Key]
    public int StockId { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int WarehouseId { get; set; }
    public Warehouse? Warehouse { get; set; }
    public int Quantity { get; set; }
}

public class WorkOrder
{
    public int WorkOrderId { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int PlannedQuantity { get; set; }
    public int PlannedTimeMin { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string Status { get; set; } = "Pending";
}

public class RfidTags
{
    [Key]
    public int RfidTagId { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int? SerialNo { get; set; }
}

public class InventoryMovements
{
    [Key]
    public int MovementId { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int WarehouseId { get; set; }
    public Warehouse? Warehouse { get; set; }
    public string MovementType { get; set; } = "IN"; // IN or OUT
    public int Quantity { get; set; }
    public DateTime MovementDate { get; set; } = DateTime.Now;
}