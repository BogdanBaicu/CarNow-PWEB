using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;

public class Reservation : BaseEntity
{
    public ReservationStatusEnum Status { get; set; } = default!;
    public DateOnly StartDate { get; set; } = default!;
    public DateOnly EndDate { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public Car Car { get; set; } = default!;
    public Guid CarId { get; set; } = default!;
    public User Employee { get; set; } = default!;
    public Guid EmployeeId { get; set; } = default!;
}
