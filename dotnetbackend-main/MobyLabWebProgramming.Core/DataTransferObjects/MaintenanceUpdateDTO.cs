namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record MaintenanceUpdateDTO(
       Guid Id,
          DateOnly? MaintenanceDate = default,
             string? Description = default,
                decimal? Price = default,
                   string? ServiceName = default);