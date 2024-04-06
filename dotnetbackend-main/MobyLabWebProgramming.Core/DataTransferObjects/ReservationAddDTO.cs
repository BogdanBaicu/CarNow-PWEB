using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class ReservationAddDTO
{
    public ReservationStatusEnum Status { get; set; } = default!;
    public DateOnly StartDate { get; set; } = default!;
    public DateOnly EndDate { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public Guid CarId { get; set; } = default!;
    public Guid EmployeeId { get; set; } = default!;
    public Guid CustomerId { get; set; } = default!;
}
