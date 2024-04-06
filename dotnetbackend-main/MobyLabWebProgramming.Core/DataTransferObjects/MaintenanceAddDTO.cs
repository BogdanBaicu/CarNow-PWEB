﻿namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class MaintenanceAddDTO
{
    public DateOnly MaintenanceDate { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public string ServiceName { get; set; } = default!;
    public Guid CarId { get; set; }
}