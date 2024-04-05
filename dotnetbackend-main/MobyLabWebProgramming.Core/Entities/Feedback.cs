using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;

public class Feedback : BaseEntity
{
    public string Description { get; set; } = default!;
    public int CarRating { get; set; } = default!;
    public int EmployeeRating { get; set; } = default!;
    public Reservation Reservation { get; set; } = default!;
    public Guid ReservationId { get; set; } = default!;

}
