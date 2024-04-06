using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class FeedbackDTO
{
    public Guid Id { get; set; }
    public string Description { get; set; } = default!;
    public int CarRating { get; set; }
    public int EmployeeRating { get; set; }
    public Reservation Reservation { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
