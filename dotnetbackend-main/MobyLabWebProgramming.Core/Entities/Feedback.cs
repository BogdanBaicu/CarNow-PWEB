using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;

public class Feedback : BaseEntity
{
    public string Description { get; set; } = default!;
    public int CarRating { get; set; } = default!;
    public int EmployeeRating { get; set; } = default!;
    public Car Car { get; set; } = default!;
    public Guid CarId { get; set; } = default!;
    public User Employee { get; set; } = default!;
    public Guid EmployeeId { get; set; } = default!;
}
