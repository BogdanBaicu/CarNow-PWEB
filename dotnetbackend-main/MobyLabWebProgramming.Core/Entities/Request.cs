using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;

public class Request : BaseEntity
{
    public ReservationStatusEnum Status { get; set; } = default!;
    public DateOnly StartDate { get; set; } = default!;
    public DateOnly EndDate { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public Car Car { get; set; } = default!;
    public Guid CarId { get; set; } = default!;
    public User Customer { get; set; } = default!;
    public Guid CustomerId { get; set; } = default!;
}
