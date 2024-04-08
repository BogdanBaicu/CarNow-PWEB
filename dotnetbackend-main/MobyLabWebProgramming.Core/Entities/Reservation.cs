using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;

public class Reservation : BaseEntity
{
    public ReservationStatusEnum Status { get; set; } = default!;
    public User Employee { get; set; } = default!;
    public Guid EmployeeId { get; set; } = default!;
    public Request Request { get; set; } = default!;
    public Guid RequestId { get; set; } = default!;
}
