using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class MaintenanceDTO
{
    public Guid Id { get; set; }
    public DateOnly MaintenanceDate { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public string ServiceName { get; set; } = default!;



    public Guid CarId { get; set; }
    public Car Car { get; set; } = default!;



    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}