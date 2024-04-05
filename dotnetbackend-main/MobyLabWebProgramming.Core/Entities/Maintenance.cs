namespace MobyLabWebProgramming.Core.Entities;

public class Maintenance : BaseEntity
{
    public DateOnly MaintenanceDate { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public string ServiceName { get; set; } = default!;
}
